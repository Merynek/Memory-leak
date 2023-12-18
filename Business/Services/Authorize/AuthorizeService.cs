using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Common.AppSettings;
using Common.Dto;
using Common.Enums;
using Common.Exceptions;
using Common.Singletons;
using Data.Context;
using Data.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Business.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly SUBDbContext _context;
        private readonly JWTSettings _jwtsettings;
        private readonly IEmailService _emailService;
        private readonly IBrevoService _brevoService;
        private readonly IAppCache _appCache;

        public AuthorizeService(SUBDbContext context, IAppCache appCache, IEmailService emailService, 
            IOptions<JWTSettings> jwtsettings, IBrevoService brevoService)
        {
            _context = context;
            _jwtsettings = jwtsettings.Value;
            _appCache = appCache;
            _emailService = emailService;
            _brevoService = brevoService;
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto req)
        {
            var user = await _context.User
                .Where(user => user.Email == req.Email && user.Password == _hash(req.Password))
                .FirstOrDefaultAsync();

            if (user == null)
            {
                throw new BadRequestSUBException(ErrorCode.LOGIN_FAILED, "Login failed");
            }
            if (user.Banned)
            {
                throw new BadRequestSUBException(ErrorCode.BANNED, "User is banned");
            }
            if (!user.Active)
            {
                throw new BadRequestSUBException(ErrorCode.INACTIVE, "User is not activated");
            }
            await _context.SaveChangesAsync();
            var response = new LoginResponseDto
            {
                user = new CurrentUserDto(user.Id, user.Email, user.Role),
                appBusinessConfig = _appCache.ConfigCache,
                Token = _createToken(user)
            };

            return response;
        }

        public async Task ForgetPassword(ForgetPasswordRequestDto req)
        {
            var user = await _context.User
                .Where(user => user.Email == req.Email).FirstOrDefaultAsync();

            if (user != null)
            {
                var token = _generateRandomToken();
                var e = new PasswordTokenEntity();
                e.Token = token;
                e.UserId = user.Id;

                _context.PasswordToken.Add(e);
                await _context.SaveChangesAsync();
                await _emailService.SendChangePassword(req.Email, token);
                return;
            }

            throw new BadRequestSUBException(ErrorCode.UNKNOWN_EMAIL, "Unknown email");
        }

        public async Task ChangePassword(ChangePasswordRequestDto req)
        {
            var token = await _context.PasswordToken
                .Where(e => e.Token == req.Token).FirstOrDefaultAsync();

            if (token == null)
            {
                throw new BadRequestSUBException(ErrorCode.INVALID_PASSWORD_TOKEN, "Invalid token");
            }

            var user = await _context.User
                .Where(user => user.Id == token.UserId).FirstOrDefaultAsync();

            if (user != null)
            {
                user.Password = _hash(req.NewPassword);
                _context.Entry(user).State = EntityState.Modified;
                _context.PasswordToken.Remove(token);
                await _context.SaveChangesAsync();
                return;
            }
            throw new BadRequestSUBException(ErrorCode.UNKNOWN_USER, "Unknown user");
        }

        public async Task RegistrationUser(RegistrationUserRequestDto req)
        {
            var email = await _context.User
                .Where(u => u.Email == req.Email).FirstOrDefaultAsync();

            if (email != null)
            {
                throw new BadRequestSUBException(ErrorCode.USER_EXISTS, "User exists");
            }

            var user = new UserEntity(req) { Password = _hash(req.Password) };
            user.Notifications.Add(Notifications.NEW_OFFER_FROM_TRANSPORTER);
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            var activeToken = new UserActiveTokenEntity();
            activeToken.UserId = user.Id;
            activeToken.Token = _generateRandomToken();
            _context.UserActiveToken.Add(activeToken);
            await _context.SaveChangesAsync();
            await _emailService.SendRegistration(user.Id, activeToken.Token);
            await _brevoService.Subscribe(user);
        }

        public async Task ActiveUser(UserActiveRequestDto req)
        {
            var token = await _context.UserActiveToken
                .Where(u => u.Token == req.Token).FirstOrDefaultAsync();

            if (token == null)
            {
                throw new BadRequestSUBException(ErrorCode.UNKNOWN_USER, "Token not found or user not exists");
            }
            var user = await _context.User
                .Where(u => u.Id == token.UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new BadRequestSUBException(ErrorCode.UNKNOWN_USER, "User not found");
            }
            user.Active = true;
            _context.UserActiveToken.Remove(token);
            await _context.SaveChangesAsync();
        }

        private AccessTokenDto _createToken(UserEntity user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtsettings.SecretKey);
            var expire = DateTime.UtcNow.AddDays(1);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(_createClaims(user)),
                Expires = expire,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = new AccessTokenDto();
            accessToken.Token = tokenHandler.WriteToken(token);
            accessToken.ExpireDate = expire;

            return accessToken;
        }

        private Claim[] _createClaims(UserEntity user)
        {
            return new Claim[]
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, Enum.GetName(typeof(UserRole), user.Role))
            };
        }

        private string _hash(string input)
        {
            using (var hashAlgorithm = SHA1.Create())
            {
                var hash = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        private string _generateRandomToken()
        {
            var length = 12;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            using (var rng = RandomNumberGenerator.Create())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);

                var charsToTake = new char[length];
                for (var i = 0; i < length; i++)
                {
                    charsToTake[i] = chars[bytes[i] % chars.Length];
                }

                return new string(charsToTake);
            }
        }
    }
}
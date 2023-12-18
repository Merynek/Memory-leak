using Common.Dto;
using System.Threading.Tasks;

namespace Business.Services
{
    public interface IAuthorizeService
    {
        Task<LoginResponseDto> Login(LoginRequestDto req);

        Task ForgetPassword(ForgetPasswordRequestDto req);

        Task ChangePassword(ChangePasswordRequestDto req);

        Task RegistrationUser(RegistrationUserRequestDto req);

        Task ActiveUser(UserActiveRequestDto req);
    }
}

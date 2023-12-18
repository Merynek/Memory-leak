using AutoMapper;
using Business;
using Business.Services;
using Common.AppSettings;
using Common.Singletons;
using Data.Context;
using Sub.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Hangfire;
using Hangfire.SqlServer;
using Hangfire.Dashboard;
using Data.Entity;
using Common.Dto;

namespace Server_SUB
{

    public class MyAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context) => true;
    }

    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                        );
            });
            services.AddControllers().AddControllersAsServices();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.Configure<ClientSettings>(Configuration.GetSection("ClientSettings"));
            services.Configure<CompanyInfo>(Configuration.GetSection("CompanyInfo"));

            services.AddSwaggerGen(setup =>
            {
                // Include 'SecurityScheme' to use JWT Authentication
                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Description = "Put ONLY your JWT Bearer token on textbox below!",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                setup.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {jwtSecurityScheme, Array.Empty<string>()}
                });


                setup.SwaggerDoc("v1", new OpenApiInfo { Title = "My API Merin", Version = "v1" });
            });

            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(Configuration.GetConnectionString("HangfireConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            }).AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.Converters.Add(new StringEnumConverter());
                opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddSwaggerGenNewtonsoftSupport();

            Console.WriteLine(Configuration.GetConnectionString("SubDB"));

            services.AddDbContext<SUBDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("SubDB")));

            // Scoped
            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddScoped<ISchedulerTaskService, SchedulerTaskService>();
            services.AddScoped<IEmailConfigService, EmailConfigService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<ITripService, TripService>();

            // Singletons
            services.AddSingleton<IAppCache, AppCache>();
            services.AddSingleton<ILocker, Locker>();

            // Transients
            services.AddTransient<IBrevoService, BrevoService>();
            services.AddTransient<IEmailParamsService, EmailParamsService>();
            services.AddTransient<ITripBackgroundTasks, TripBackgroundTasks>();


            var config = new MapperConfiguration(c => { c.AddProfile<MappingProfile>(); });

            services.AddSingleton<IMapper>(s => config.CreateMapper());

            services.AddAutoMapper(typeof(Startup));

            services.AddHttpContextAccessor();

            SetUpAuthentication(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app,
            IWebHostEnvironment env,
            IHostApplicationLifetime lifetime,
            IServiceProvider services)
        {
            using (var scope = services.CreateScope())
            {
                var service = scope.ServiceProvider;
                var dbContext = service.GetRequiredService<SUBDbContext>();
                var loggerFactory = service.GetRequiredService<ILoggerFactory>();

                app.UsePathBase("/dotnet");

                app.UseStaticFiles();

                app.UseStaticFiles(new StaticFileOptions()
                {
                    FileProvider = new PhysicalFileProvider(
                        Path.Combine(Directory.GetCurrentDirectory(), "Public")),
                    RequestPath = new PathString("/Public")
                });

                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API Merin"); });

                if (env.IsDevelopment())
                {
                    app.UseDeveloperExceptionPage();
                }
                else
                {
                    dbContext.Database.Migrate();
                }

                app.UseErrorHandlingMiddleware();

                SetupLogging(loggerFactory);

                lifetime.ApplicationStarted.Register(() => OnApplicationStarted(app));

                app.UseHttpsRedirection();

                app.UseRouting();

                app.UseAuthentication();

                app.UseAuthorization();

                app.UseCors("CorsPolicy");

                app.UseRequestFilter();

                app.UseEndpoints(endpoints => {
                    endpoints.MapControllers();
                });

                app.Use((context, next) =>
                {
                    var path = context.Request.Path;
                    if (path.StartsWithSegments("/hangfire"))
                    {
                        context.Request.PathBase = "";
                    }

                    return next();
                });

                app.UseHangfireDashboard("/hangfire", new DashboardOptions()
                {
                    Authorization = new[] { new MyAuthorizationFilter() }
                });
            }
        }

        private void OnApplicationStarted(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<SUBDbContext>();
                var appChache = serviceScope.ServiceProvider.GetRequiredService<IAppCache>();
                var mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();
                var dbUsers = dbContext.User.Where(u => u.Banned || !u.Active).ToList();

                var users = mapper.Map<List<UserEntity>, List<UserDto>>(dbUsers);
                if (users.Any())
                {
                    appChache.CreateBanUserCache(users);
                }
                var config = dbContext.AppBusinessConfig.FirstOrDefault();
                appChache.SetAppConfig(config.ConvertToResponse());
            }
        }

        private void SetUpAuthentication(IServiceCollection services)
        {
            var jwtSection = Configuration.GetSection("JWTSettings");
            services.Configure<JWTSettings>(jwtSection);

            var appSettings = jwtSection.Get<JWTSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = true;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }

        private void SetupLogging(ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile("Logs/INFO-{Date}.txt", LogLevel.Information);
            loggerFactory.AddFile("Logs/ERROR-{Date}.txt", LogLevel.Error);
            loggerFactory.AddFile("Logs/CRITIC-{Date}.txt", LogLevel.Critical);
        }
    }
}

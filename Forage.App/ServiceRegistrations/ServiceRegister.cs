using FluentValidation;
using FluentValidation.AspNetCore;
using Forage.Core.Entities;
using Forage.Core.Options;
using Forage.Core.Repositories;
using Forage.Data.Context;
using Forage.Data.Repositories;
using Forage.Service.Profiles.Companys;
using Forage.Service.Services.Implementations;
using Forage.Service.Services.Interfaces;
using Forage.Service.Validations.Questions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using TokenHandler = Forage.Service.Services.Implementations.TokenHandler;

namespace Forage.App.ServiceRegistrations
{
    public static class ServiceRegister
    {
        public static void Register(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddControllers()?.AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<QuestionPostDtoValidation>());
            services.AddAutoMapper(typeof(CompanyProfile));
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            #region AddScoped

            services.AddScoped<ISubscribeRepository, SubscribeRepository>();
            services.AddScoped<ISubscribeService, SubscribeService>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<IIdentityUserService, IdentityUserService>();
            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IInternRepository, InternRepository>();
            services.AddScoped<IInternService, InternService>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<ITokenHandler, TokenHandler>();
            services.AddScoped<ICourseLessonRepository, CourseLessonRepository>();
            services.AddScoped<ICourseLessonService, CourseLessonService>();
            services.AddScoped<ICourseLessonActivityRepository, CourseLessonActivityRepository>();
            services.AddScoped<ICourseLessonActivityService, CourseLessonActivityService>();
            services.AddScoped<ICourseLessonLevelRepository, CourseLessonLevelRepository>();
            services.AddScoped<ICourseLessonLevelService, CourseLessonLevelService>();
            services.AddScoped<ICourseSkillRepository, CourseSkillRepository>();
            services.AddScoped<ICompanyIndustryFieldService, CompanyIndustryFieldService>();
            services.AddScoped<ICompanyIndustryFieldRepository, CompanyIndustryFieldRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();
            services.AddScoped<IQuestionService, QuestionService>();
            services.AddScoped<ICourseLevelRepository, CourseLevelRepository>();
            services.AddScoped<ICourseLevelService, CourseLevelService>();
            services.AddScoped<ICourseCategoryRepository, CourseCategoryRepository>();
            services.AddScoped<ICourseCategoryService, CourseCategoryService>();
            services.AddScoped<IHelpMessageTypeService, HelpMessageTypeService>();
            services.AddScoped<IHelpMessageTypeRepository, HelpMessageTypeRepository>();
            services.AddScoped<IHelpMessageService, HelpMessageService>();
            services.AddScoped<IHelpMessageRepository, HelpMessageRepository>();

            #endregion
            services.AddDbContext<ForageAppDbContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("Default"));
            });
            services.AddControllers().AddJsonOptions(x =>
                            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
            services.AddIdentity<AppUser, IdentityRole>()
                      .AddDefaultTokenProviders()
                             .AddEntityFrameworkStores<ForageAppDbContext>();
            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 3;
                options.Lockout.AllowedForNewUsers = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireDigit = true;
                //options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
            });

            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation  
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "JWT Token Authentication API",
                    Description = "ASP.NET Core 3.1 Web API"
                });
                // To Enable authorization using Swagger (JWT)  
                swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                });
                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] {}
                    }
                });
            });

            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtTokenSettings:ValidIssuer"],
                    ValidAudience = configuration["JwtTokenSettings:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtTokenSettings:SignInKey"])) //Configuration["JwtToken:SecretKey"]  
                };
            });
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

        }
    }


}

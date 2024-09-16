using CEG_BAL.AutoMapperProfile;
using CEG_BAL.Services.Implements;
using CEG_BAL.Services.Interfaces;
using CEG_DAL.Infrastructure;
using CEG_DAL.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using System.Text.Json.Serialization;

namespace CEG_WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services for web app.

            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                ConfigureDevelopmentPipeline(app);
            }

            ConfigurePipeline(app);

            app.MapControllers();

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration config)
        {
            // Add authentication services
            AddAuthenticationServices(services, config);

            // Add MVC services
            AddMvcServices(services);

            // Add Swagger services
            AddSwaggerServices(services);

            // Add Razor Pages services
            AddRazorPagesServices(services);

            // Add CORS services
            AddCorsServices(services);

            // Add AutoMapper
            services.AddAutoMapper(typeof(MappingProfile));
            /*services.AddDbContext<MyDBContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });*/

            // Register custom services
            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IEnrollService, EnrollService>();
            services.AddScoped<IGameConfigService, GameConfigService>();
            services.AddScoped<IGameLevelService, GameLevelService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IHomeworkResultService, HomeworkResultService>();
            services.AddScoped<IHomeworkService, HomeworkService>();
            services.AddScoped<IJWTService, JWTService>();
            services.AddScoped<IParentService, ParentService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IRegisteredCourseService, RegisteredCourseService>();
            services.AddScoped<ISessionService, SessionService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ITeacherService, TeacherService>();
        }

        private static void AddSwaggerServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Children English Game API Documentation",
                    Version = "v1",
                });
                // Configuring JWT Validation for Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Bearer token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securityScheme);
                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new[] { "Bearer" } }
                };
                c.AddSecurityRequirement(securityRequirement);
            });
        }

        private static void AddAuthenticationServices(IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config.GetSection("AppSettings:SecretKey").Value)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    /*ValidIssuer = config.GetSection("AppSettings:ValidIssuer").Value,
                    ValidAudience = config.GetSection("AppSettings:ValidAudience").Value,*/
                };
            });
        }

        private static void AddCorsServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });
        }
        private static void AddRazorPagesServices(IServiceCollection services)
        {
            services.AddRazorPages().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
            });
        }
        private static void AddMvcServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        private static void ConfigureDevelopmentPipeline(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Children English Game Web API");
            });
        }

        private static void ConfigurePipeline(WebApplication app)
        {
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors();
        }
    }
}
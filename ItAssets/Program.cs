
using Itasset.Application.Interfaces;
using Itasset.Application.Settings;
using Itassets.Infrastructure.Common.Mappings;
using Itassets.Infrastructure.Entities;
using Itassets.Infrastructure.Interfaces;
using Itassets.Infrastructure.Presistance;
using Itassets.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace ItAssets
{
    public class Program
    {
        public static async Task Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            var jwtSettingsSection = builder.Configuration.GetSection("JwtSettings");
            builder.Services.Configure<JwtSettings>(jwtSettingsSection);
            var jwtSettings = jwtSettingsSection.Get<JwtSettings>();


            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly("Itassets.Infrastructure")
                )
            );
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
            })


           .AddEntityFrameworkStores<ApplicationDbContext>()
           .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = "JwtBearer";
                options.DefaultChallengeScheme = "JwtBearer";
            })
            .AddJwtBearer("JwtBearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                    RoleClaimType = ClaimTypes.Role
                };
            });

            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                serverOptions.ListenAnyIP(41335);
            });
            builder.Services.AddAutoMapper(typeof(MappingProfile));


            builder.Services.AddScoped<IUserService, UserService>();

            builder.Services.AddScoped<IEmailSender, SendGridEmailSender>();

            builder.Services.AddScoped<IBrand, BrandService>();

            builder.Services.AddScoped<ICategory, CategoryService>();

            builder.Services.AddScoped<IOffice, OfficeService>();

            builder.Services.AddScoped<ISupplier, SupplierService>();

            builder.Services.AddScoped<IEmployee, EmployeeService>();

            builder.Services.AddScoped<IDevice, DeviceService>();

            builder.Services.AddScoped<IDeviceRequest, DeviceRequestsService>();

            builder.Services.AddScoped<IDeviceEmployeeAssignment, EmployeeDeviceAssignService>();

            builder.Services.AddScoped<IDeviceOfficeAssignment, OfficeDeviceAssignService>();


            builder.Services.AddScoped<IDeviceTransfer, DeviceTransferService>();

            builder.Services.AddScoped<IDeviceMaintainanceSchedule, DeviceMaintainanceScheduleService>();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = { "Admin", "Member" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Startup failed: " + ex.Message);
                throw;
            }
        }
    }
}

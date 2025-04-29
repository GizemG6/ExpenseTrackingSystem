using ExpenseTrackingSystem.API.Middlewares;
using ExpenseTrackingSystem.Application;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using ExpenseTrackingSystem.Infrastructure;
using ExpenseTrackingSystem.Infrastructure.Services;
using ExpenseTrackingSystem.Persistence;
using ExpenseTrackingSystem.Persistence.Data;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Core;
using Serilog.Sinks.MSSqlServer;
using System.Security.Claims;
using System.Text;

namespace ExpenseTrackingSystem.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			#region Services
			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddPersistenceServices(builder.Configuration);
			builder.Services.AddInfrastructureServices(builder.Configuration);
			#endregion

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();

			#region Serilog Configuration
			var columnOptions = new ColumnOptions();

			Logger log = new LoggerConfiguration()
				.WriteTo.Console()
				.WriteTo.File("logs/log.txt")
				.WriteTo.MSSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
				sinkOptions: new MSSqlServerSinkOptions { TableName = "Logs", AutoCreateSqlTable = true },
				columnOptions: columnOptions)
				.Enrich.FromLogContext()
				.MinimumLevel.Information()
				.CreateLogger();

			builder.Host.UseSerilog(log);
			#endregion

			#region Swagger Configuration
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
								  "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
								  "Example: \"Bearer 12345abcdef\""
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
						Array.Empty<string>()
					}
				});
			});
			#endregion

			#region JWT Authentication
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new()
					{
						ValidateAudience = true,
						ValidateIssuer = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,

						ValidAudience = builder.Configuration["Token:Audience"],
						ValidIssuer = builder.Configuration["Token:Issuer"],
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
						LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
						{
							return expires != null && expires > DateTime.UtcNow;
						},
						NameClaimType = ClaimTypes.Name
					};
				});
			#endregion

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

			app.UseHttpsRedirection();

			app.UseHangfireDashboard("/hangfire");

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}

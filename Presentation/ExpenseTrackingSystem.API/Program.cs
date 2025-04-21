
using ExpenseTrackingSystem.API.Middlewares;
using ExpenseTrackingSystem.Application;
using ExpenseTrackingSystem.Persistence;

namespace ExpenseTrackingSystem.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.AddPersistenceServices(builder.Configuration);

			builder.Services.AddControllers();
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.ConfigureExceptionHandler(app.Services.GetRequiredService<ILogger<Program>>());

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}

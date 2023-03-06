using bluestone.guests.business.Services.Reviews.V1;
using bluestone.guests.data;
using bluestone.guests.data.Repositories;
using bluestone.guests.data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace bluestoneguests.api
{
    public class Program
    {
    public static void Main(string[] args)
      {
      var builder = WebApplication.CreateBuilder(args);

      string ConnectionString = builder.Configuration["ConnectionStrings:BluestoneReviews"] ?? "";

      builder.Services.AddDbContextFactory<GuestReviewsDbContext>(options => options.UseSqlServer(ConnectionString, sql => sql.MigrationsAssembly("bluestone.guests.data")));



      // Add services to the container.
      builder.Services.AddRazorPages();
      builder.Services.AddControllers();
      // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
      builder.Services.AddEndpointsApiExplorer();
      builder.Services.AddSwaggerGen();

      builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
      builder.Services.AddScoped<IGuestReviewService, GuestReviewService>();





      var app = builder.Build();

      // Configure the HTTP request pipeline.
      if (app.Environment.IsDevelopment())
        {
        app.UseSwagger();
        app.UseSwaggerUI();
        }

      app.UseHttpsRedirection();

      app.UseAuthorization();

      app.MapControllers();
      app.MapRazorPages();

      app.Run();
      }
    }
  }
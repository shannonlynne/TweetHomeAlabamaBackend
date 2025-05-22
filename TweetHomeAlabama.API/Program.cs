using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TweetHomeAlabama.Application.Interfaces;
using TweetHomeAlabama.Application.Services;
using TweetHomeAlabama.Data.DataContext;

Batteries.Init();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is not null)
    builder.Services.AddDbContext<TweetHomeAlabamaDbContext>(options =>
        options.UseSqlite(connectionString));

builder.Services.AddScoped<IBirdService, BirdService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthorization();

app.UseCors(options => options.WithOrigins("http://localhost:4200")
.AllowAnyMethod()
.AllowAnyHeader());

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseRouting();

app.MapControllers();

app.Run();




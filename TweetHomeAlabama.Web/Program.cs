using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using TweetHomeAlabama.Application.Interfaces;
using TweetHomeAlabama.Application.Services;
using TweetHomeAlabama.Data.DataContext;

Batteries.Init();

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddMvc();

builder.Services.AddScoped<IBirdService, BirdService>();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (connectionString is not null)
    builder.Services.AddDbContext<TweetHomeAlabamaDbContext>(options =>
        options.UseSqlite(connectionString));

builder.Services.AddTransient<IBirdService, IBirdService>();

WebApplication app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

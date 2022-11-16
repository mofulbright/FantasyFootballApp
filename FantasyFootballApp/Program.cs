using FantasyFootballApp;
using FantasyFootballApp.Data;
using FantasyFootballApp.Repositories;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<IDbConnection>((s) =>
    {
        IDbConnection conn = new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection"));
        conn.Open();
        return conn;
    });
builder.Services.AddScoped<string>((s) =>
{
    var keys = File.ReadAllText("appsettings.json");
    string key = JObject.Parse(keys)["ConnectionStrings"]["SportsDataKey"].ToString();
    return key;
});
builder.Services.AddTransient<APIClient>();
builder.Services.AddTransient<IFantasyTeamRepository, FantasyTeamRepository>();
builder.Services.AddTransient<ITeamAndPlayerRepository, TeamAndPlayerRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

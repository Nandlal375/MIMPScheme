//using DNTCaptcha.Core;

using MIMPScheme.Repository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10); // Set session timeout
});

builder.Services.AddScoped<LoginRepository>();


//MSSQL DB Conn:
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

//builder.Services.AddConnections<LoginRepository>(options => options.UseSqlServer(connString));
//builder.Services.AddSingleton<LoginRepository>();
//builder.Services.AddSingleton<LoginRepository>(_=> new LoginRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
//string connString = builder.Configuration.GetConnectionString("DefaultConnection");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

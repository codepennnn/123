using Microsoft.EntityFrameworkCore;
 using Log_Innovation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews();
var Provider = builder.Services.BuildServiceProvider();
var Config = Provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<INNOVATIONDBContext>(item => item.UseSqlServer(Config.GetConnectionString("DBCS")));
builder.Services.AddDbContext<UserLoginDBContext>(item => item.UseSqlServer(Config.GetConnectionString("UserDB")));

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
    pattern: "{controller=Master}/{action=HomePage}/{id?}");

var UploadPath = builder.Configuration["FileUpload:Path"];
if (!Directory.Exists(UploadPath))
{
    Directory.CreateDirectory(UploadPath);
}

app.Run();

using Dokan.DataAccess.Data;
using Dokan.DataAccess.ReposIplementaion;
using Dokan.Models.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add Razor runtime compilation for development environment
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

// register Dbcontext with dependency injection
builder.Services.AddDbContext<DokanDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DokanConnectionString")));

// Register UnitOfWork with dependency injection
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Admin}/{controller=Category}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();

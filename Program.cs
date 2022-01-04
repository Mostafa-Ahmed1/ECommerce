using ECommerce.BLL.Repository;
using ECommerce.BLL.Interfaces;
using ECommerce.DAL.Database;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using ECommerce.BLL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//config for connection string
builder.Services.AddDbContextPool<ProductContext>(opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("ConnecStr")));
// config for dependecies
builder.Services.AddScoped<IproductRep, ProductRep>();
//auto mapper
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

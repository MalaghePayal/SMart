using Microsoft.EntityFrameworkCore;
using SMart.Models;
using Microsoft.AspNetCore.Identity;
using SMart.Data;
using SMart.Areas.Identity.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<ApplicationContextDb>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<SMartIdentityContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDefaultIdentity<SMartIdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<SMartIdentityContext>();

builder.Services.AddRazorPages();
// Register custom policies for authorization based on claims
builder.Services.AddAuthorization(options =>
// Inventory policy(Inventory is name of policy here): allows users with the "Position" claim set to value "InventoryManager"
//InventoryManager is claim value here.
{
    options.AddPolicy("Inventory", p => p.RequireClaim("Position", "InventoryManager"));
    // Cashiers policy: allows users with the "Position" claim set to "Cashier"
    options.AddPolicy("Cashiers", p => p.RequireClaim("Position", "Cashier"));
});
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
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

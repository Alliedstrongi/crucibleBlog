using crucibleBlog.Controllers;
using crucibleBlog.Data;
using crucibleBlog.Models;
using crucibleBlog.Services.Interfaces;
using crucibleBlog.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = DataUtility.GetConnectionString(builder.Configuration)
	?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// database service
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// error service
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
 
// Identity service
builder.Services.AddIdentity<BlogUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<ApplicationDbContext>();

// MVC Routing Service
builder.Services.AddMvc();

//ImageService
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<IBlogService, BlogService>();

// Custom Services
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

var app = builder.Build();

var scope = app.Services.CreateScope();
await DataUtility.ManageDataAsync(scope.ServiceProvider);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
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
    name: "custom",
    pattern: "Content/{slug}",
    defaults: new { controller = "BlogPosts", action = "Details" }
    );  

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Training.AspNet.Middlewares.App.Data;
using Training.AspNet.Middlewares.App.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts(); //adds the Strict-Transport-Security header.
}

//This middleware is used to redirects HTTP requests to HTTPS.  
app.UseHttpsRedirection();
//This middleware is used to returns static files and short-circuits further request processing.
app.UseStaticFiles();

//This middleware is used to route requests.
app.UseRouting();



//This middleware is used to authorizes a user to access secure resources.  
app.UseAuthentication();
app.UseAuthorization();

app.UseLogUrl();

// Просмотр файлов.
app.UseDirectoryBrowser(new DirectoryBrowserOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css")),
    RequestPath = "/css"
});

//This middleware is used to add Razor Pages endpoints to the request pipeline.    
app.MapRazorPages();




app.Run();

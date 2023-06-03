using HoursBank.Web.Controllers;
using HoursBank.Web.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapControllerRoute(
                    name: "login",
                    pattern: "api/Login",
                    defaults: new { controller = "Login", action = "Login" });

/*
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller}/{action=Index}/{id?}");

    endpoints.MapPost("api/Login", async context =>
    {
        var loginModel = await context.Request.ReadFromJsonAsync<LoginModel>();
        var loginController = new LoginController(new HttpClient());
        var result = await loginController.Login(loginModel);
        var response = context.Response;
        response.ContentType = "application/json";
        await response.WriteAsJsonAsync(result);
    });
});
*/
app.MapFallbackToFile("index.html"); ;

app.Run();

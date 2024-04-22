using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using PasechnikovaPR33p18.Models;
using PasechnikovaPR33p18.Pages;
using PasechnikovaPR33p18.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PostsContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("local")));
builder
    .Services
    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/Login";
        opt.Cookie.Name = "NotesUser";
        opt.ExpireTimeSpan = TimeSpan.FromHours(3);
    });
builder.Services.AddScoped<IPostsService, PostsService>( );
builder.Services.AddScoped<IPasswordService, Sha256PasswordService>( );
builder.Services.AddScoped<IUserService, UserService>( );
builder.Services.AddScoped<INotifyService, SessionNotifyService>();
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddRazorPages( );
var app = builder.Build( );
app.UseStaticFiles( );
app.UseSession();
app.UseRouting( );
app.UseAuthentication( );
app.UseAuthorization( );
app.MapGet("/Logout", async context => {
    await context.SignOutAsync( );
    context.Response.Redirect("/");
});
app.MapRazorPages( );
app.Run( );

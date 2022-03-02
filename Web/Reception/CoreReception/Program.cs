using Microsoft.AspNetCore.Authentication.Cookies;
using WebCore.Extension;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddMvc().AddNewtonsoftJson();
builder.Services.AddCors();
builder.Services.AddResponseCompression();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Forbidden/";
        options.LoginPath = new PathString("/Login/Index");
        options.LogoutPath = new PathString("/Login/CheckLogout");
    });
//builder.Services.AddSingleton<ITicketStore, MyRedisTicketStore>();
//builder.Services.AddOptions<CookieAuthenticationOptions>("Cookies")
//     .Configure<ITicketStore>((o, t) => o.SessionStore = t);

builder.WebHost.UseUrls("http://*:18020");

HttpContextHelper.Register(builder.Services);
var app = builder.Build();
var env = app.Environment;
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
HttpContextHelper.Initialize(app, env);
//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
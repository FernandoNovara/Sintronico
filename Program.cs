using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sintronico.Models;

var builder = WebApplication.CreateBuilder(args);
var Configuration = builder.Configuration;

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.LoginPath = "/Usuario/Login";
            options.LogoutPath = "/Usuario/Logout";
            options.AccessDeniedPath = "/Home/Restringido/";
        })        .AddJwtBearer(options =>//la api web valida con token
				{
					options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = Configuration["TokenAuthentication:Issuer"],
						ValidAudience = Configuration["TokenAuthentication:Audience"],
						IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
							Configuration["TokenAuthentication:SecretKey"])),
					};
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];
                            var path = context.HttpContext.Request.Path;
                            if (!string.IsNullOrEmpty(accessToken) &&
                                path.StartsWithSegments("/Api/Propietario/token"))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
				});

builder.Services.AddAuthorization(options => 
{
        options.AddPolicy("Administrador",
                    policy => policy.RequireRole("Administrador"));
});

builder.Services.AddDbContext<DataContext>(
    options => options.UseMySql(
        Configuration["ConnectionString:DefaultConnection"],
        ServerVersion.AutoDetect(Configuration["ConnectionString:DefaultConnection"])
    )
); 

builder.Services.AddTransient<IRepositorio<Propietario>, RepositorioPropietario>();
builder.Services.AddTransient<IRepositorioPropietario, RepositorioPropietario>();
builder.Services.AddTransient<IRepositorioBicicleta, RepositorioBicicleta>();
builder.Services.AddTransient<IRepositorioPresupuesto, RepositorioPresupuesto>();
builder.Services.AddTransient<IRepositorioDetallePresupuestos, RepositorioDetallePresupuesto>();
builder.Services.AddTransient<IRepositorioRepuesto, RepositorioRepuesto>();
builder.Services.AddTransient<IRepositorioPago, RepositorioPago>();
builder.Services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();


builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(5000);
    serverOptions.ListenAnyIP(5001,listenOptions => listenOptions.UseHttps());
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    app.MapControllerRoute( name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute( name: "Login" ,"entrar/{**accion}", new {controller = "Usuario",action = "Login"});
});

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

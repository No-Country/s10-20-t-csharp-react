using AutoMapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using quejapp.Models;
using s10.Back.Data;
using s10.Back.Data.IRepositories;
using s10.Back.Data.Repositories;
using s10.Back.Handler;
using s10.Back.Services.Auth.Models;
using System.Security.Claims;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<RedCoContext>(
   options => 
       options.UseSqlServer(builder.Configuration.GetConnectionString("s10"),
    sqlServerOptions => sqlServerOptions.UseNetTopologySuite()    
   ));

builder.Services.AddScoped<ICloudinaryService, CloudinaryHelper>();

//var automapper = new MapperConfiguration(item => item.AddProfile(new AutoMapperHandler()));
//IMapper mapper = automapper.CreateMapper();
//builder.Services.AddSingleton(mapper);
//builder.Services.AddScoped<SignInManager<AppUser>>();

#region Auth

//builder.Services.AddIdentityCore<AppUser>(options => options.SignIn.RequireConfirmedAccount = true)
//    .AddEntityFrameworkStores<RedCoContext>();

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
    options.User.RequireUniqueEmail = true;

})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<RedCoContext>();


builder.Services.AddAuthentication(options =>
{
    options.RequireAuthenticatedSignIn = true;
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    // options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
          .AddCookie(options =>
          {
              options.Events.OnRedirectToAccessDenied =
                  options.Events.OnRedirectToLogin = c =>
                  {
                      c.Response.StatusCode = StatusCodes.Status401Unauthorized;
                      return Task.FromResult<object>(null);
                  };

          })
          .AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
          {
              var googleAuth = builder.Configuration.GetSection("Authentication:Google");
              options.ClientId = googleAuth["ClientId"];
              options.ClientSecret = googleAuth["ClientSecret"];
          });

#endregion

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

builder.Services.AddControllers(options => 
{
    options.EnableEndpointRouting = false;
    options.CacheProfiles.Add("NoCache", new CacheProfile() { NoStore = true });
    options.CacheProfiles.Add("Any-60", new CacheProfile()
    {
        Location = ResponseCacheLocation.Any,
        Duration = 60
    });
});
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSingleton<GeometryFactory>(
    NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));

builder.Services.AddAuthorization();


builder.Services.AddAuthorization();
builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
//app.UseMvc(routes =>
//{
//    routes.MapRoute(
//        name: "default",
//        template: "{controller=Auth2}/{action=login}");
//});

app.MapFallbackToFile("index.html");

app.Run();

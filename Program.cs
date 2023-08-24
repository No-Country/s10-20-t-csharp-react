using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using s10.Back.Data;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<Context>(
   options => options.UseSqlServer(builder.Configuration.GetConnectionString("s10")));


#region Auth

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

//builder.Services.AddControllers()
    // .AddJsonOptions(options =>
    //options.JsonSerializerOptions
    //.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)
    ;


builder.Services.AddControllers(options => options.EnableEndpointRouting = false);
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

builder.Services.AddAuthorization();

builder.Services.AddCors(o =>
{
    o.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseStaticFiles();
app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
//app.UseAuthorization();

app.MapControllers();
app.UseMvc(routes =>
{
    routes.MapRoute(
        name: "default",
        template: "{controller=Auth2}/{action=login}");
});
app.Run();

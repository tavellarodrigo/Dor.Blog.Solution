using AutoMapper;
using Dor.Blog.Application.Interfaces;
using Dor.Blog.Application.Services;
using Dor.Blog.Domain.Entities;
using Dor.Blog.Infrastructure;
using Dor.Blog.Infrastructure.Repositories;
using Dor.Blog.Infrastructure.Utils;
using Dor.Middleware;
using Generic.Data.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme
                        {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                        },
                        new string[] { }
                    }
                });
});


//identity
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;

    options.Lockout.MaxFailedAccessAttempts = 15;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(90);

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = false;
})
.AddEntityFrameworkStores<DataContext>()
.AddSignInManager() //->>>>>
//.AddRoleManager //-->
.AddDefaultTokenProviders();


//authorization
//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("RequireAdministratorRole", policy =>
//        policy.RequireRole("Administrator"));
//});


//authentication
var secretKey = builder.Configuration.GetValue<string>("SecretKey");

builder.Services.AddAuthentication(options =>
{
options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{         
         options.TokenValidationParameters =
           new Microsoft.IdentityModel.Tokens.TokenValidationParameters
           {
               ValidateIssuerSigningKey = true,
               IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("RodrigoPaola.2905")),
               ValidateIssuer = false,
               ValidateAudience = false,
               ValidateLifetime = true,
               ClockSkew = TimeSpan.Zero
           };
     });

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy("AdminOnly",
//        policy => policy.RequireClaim("Admin"));
//});

// Dependency Injection
var connectionString = builder.Configuration.GetConnectionString("BlogConnection") ?? throw new InvalidOperationException("Connection string 'BlogConnection' not found.");

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connectionString));


//general
//builder.Services.AddScoped<IRepository, Repository>();
builder.Services.AddScoped<IBlogService, BlogService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IAuthenticationRepository, AuthenticationRepository>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();


//Mapper
var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingsProfile()); });
builder.Services.AddSingleton(mappingConfig.CreateMapper());
//MapperUtil
builder.Services.AddSingleton<IMapperUtil, MapperUtil>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
//    app.UseMiddleware<ExceptionHandlingMiddleware>();
}


//app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

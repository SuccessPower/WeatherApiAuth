using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using WeatherApiAuth.Data;
using WeatherApiAuth.Extension;
using WeatherApiAuth.Models;
using WeatherApiAuth.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen(options =>
//{
//    var securityScheme = new OpenApiSecurityScheme
//    {
//        Name = "JWT Authentication",
//        Description = "Enter a valid Jwt Bearer Token",
//        In = ParameterLocation.Header,
//        Type = SecuritySchemeType.Http,
//        Scheme = "bearer",
//        BearerFormat = "JWT",
//        Reference = new OpenApiReference
//        {
//            Id = JwtBearerDefaults.AuthenticationScheme,
//            Type = ReferenceType.SecurityScheme
//        }
//    };

//    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
//    options.AddSecurityRequirement(new OpenApiSecurityRequirement
//    {
//        {securityScheme, new string[] {} }
//    });

//});

builder.Services.AddDbContext<WeatherApiDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("WeatherCS"));
});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<WeatherApiDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

            // Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = builder.Configuration["JWT:ValidAudience"],
                    ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]))
                };
            });


builder.Services.AddTransient<IAccountRepository, AccountRepository>();

builder.Services.AddSwagger();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();    
app.UseAuthorization();

app.MapControllers();

app.Run();

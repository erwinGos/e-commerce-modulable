using appleEarStore.WebApi;
using Data;
using Data.DTO;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Stripe;
using System.Text;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
// Add services to the container.


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var stripeSettings = builder.Configuration.GetSection("StripeSettings").Get<StripeSettings>();
StripeConfiguration.ApiKey = stripeSettings.ApiKey;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowAll",
    builderOption =>
    {
        string Url = builder.Configuration.GetSection("Urls").Get<UrlsConfig>().FrontEndUrl;
        builderOption.WithOrigins(Url)
                        .AllowCredentials()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
    });
});
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Mon API", Version = "v1" });

    // Configuration de la sécurité Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header utilisant le Bearer scheme. Exemple: \"Authorization: Bearer {token}\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.ConfigureInjectionDependencyRepository();
builder.Services.ConfigureInjectionDependencyService(configuration);
builder.Services.ConfigureDBContext(configuration);
builder.Services.AddAutoMapper(typeof(MapperConfiguration));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    string test = configuration["Jwt:SecretKey"];
    options.TokenValidationParameters = new TokenValidationParameters
    {
        // ValidateIssuer = true,
        // ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["Jwt:Issuer"],
        ValidAudience = configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:SecretKey"]))
    };
});
var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<UserMiddleWare>();

app.MapControllers();

app.Run();

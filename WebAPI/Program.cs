using Application.Commons;
using Google.Apis.Auth.OAuth2;
using Infrastructures;
using Infrastructures.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Linq;
using System.Text;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

//Database connection
var configuration = builder.Configuration.Get<AppConfiguration>() ?? new AppConfiguration();
builder.Services.AddInfrastructuresService(configuration.DatabaseConnection);
builder.Services.AddSingleton(configuration);
builder.Services.AddWebAPIService();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("http://localhost:3000")
               .AllowAnyMethod()
               .AllowAnyHeader()
               .AllowCredentials();

    });
});

//Authenticate
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.JWTSection.Issuer,
            ValidAudience = configuration.JWTSection.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.JWTSection.SecretKey)),
        };
    });

//Swagger
builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

//Firebase
var google = JObject.FromObject(configuration.GoogleImage);
string g = google.ToString();
string temp = Path.GetTempFileName();
File.WriteAllText(temp, g);
Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", temp);
GoogleCredential credential = GoogleCredential.FromFile(temp);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors();
app.UseRouting();
app.UseEndpoints(endpoints =>
{

    app.MapControllers();
    endpoints.MapHub<SignalRHub>("/notificationHub");

});
app.Run();

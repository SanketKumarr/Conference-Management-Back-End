using ConferenceManagement.Business.AdminDataAccess;
using ConferenceManagement.Business.Token;
using ConferenceManagement.Business.UserDataAccess;
using ConferenceManagement.Context;
using ConferenceManagement.UserHub;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSignalR();
builder.Services.AddSession(option =>
{
    option.IdleTimeout = TimeSpan.FromMinutes(10);
    option.Cookie.HttpOnly = true;
    option.Cookie.IsEssential = true;
});
//builder.Services.AddHttpContextAccessor();

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IAdminDataAccess, AdminDataAccess>();
builder.Services.AddScoped<IUserDataAccess, UserDataAccess>();
builder.Services.AddScoped<ITokenGenerator,TokenGenerator>();
builder.Services.AddSingleton<DbContext>();
ValidateTokenWithParameters(builder.Services, builder.Configuration);

void ValidateTokenWithParameters(IServiceCollection services, ConfigurationManager configuration)
{
    var userSecretKey = configuration["JwtValidationParameters:UserSecretKey"];
    var userIssuer = configuration["JwtValidationParameters:UserIssuer"];
    var userAudience = configuration["JwtValidationParameters:UserAudience"];
    var userSecretKeyInBytes = Encoding.UTF8.GetBytes(userSecretKey);
    var userSymmetricSecurityKey = new SymmetricSecurityKey(userSecretKeyInBytes);
    var tokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidIssuer = userIssuer,

        ValidateAudience = true,
        ValidAudience = userAudience,

        ValidateIssuerSigningKey = true,
        IssuerSigningKey = userSymmetricSecurityKey,

        ValidateLifetime = true
    };
    services.AddAuthentication(u =>
    {
        u.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        u.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(u => u.TokenValidationParameters = tokenValidationParameters);

}

builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
{
    builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins("http://localhost:4200")
        .AllowCredentials()
        .SetIsOriginAllowed(_ => true);
}));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");
app.UseSession();
app.Use(async (context, next) =>
{
    var tokenInfo = context.Session.GetString("Token");
    context.Request.Headers.Add("Authorization", "Bearer " + tokenInfo);
    await next();
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<StatusHub>("/statusHub");

});

app.Run();

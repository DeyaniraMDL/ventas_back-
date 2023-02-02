using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WSVenta.Models.Common;
using WSVenta.Services;
using WSVenta.Tools;

var MiCors = "MiCors";
var builder = WebApplication.CreateBuilder(args);



// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MiCors,
                      policy =>
                      {
                          policy.WithMethods("*");
                          policy.WithHeaders("*");
                          policy.WithOrigins("*")
                          .WithMethods("PUT", "POST", "DELETE", "GET")
                          .SetIsOriginAllowedToAllowWildcardSubdomains();
                          
                      });
});


builder.Services.AddControllers();
    /*.AddJsonOptions(options => {
        options.JsonSerializerOptions.Converters.Add(new IntToStringConverter());
        options.JsonSerializerOptions.Converters.Add(new DecimalToStringConverter());
    });*/

var appSettingsSection = builder.Configuration.GetSection("AppSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

//jwt
var appSettings = appSettingsSection.Get<AppSettings>();
var llave = Encoding.ASCII.GetBytes(appSettings.Secreto);

builder.Services.AddAuthentication(d =>
{
    d.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    d.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(d =>
    {
        d.RequireHttpsMetadata = false;
        d.SaveToken = true;
        d.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(llave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });    

builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<IVentaService, VentaService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MiCors);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

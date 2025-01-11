using DatabaseCoreKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using WebAPIGateway.Services.Authentication;
using WebAPIGateway.Services.CryptographicService;
using WebAPIGateway.Services.Products;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder => {
        builder.AllowAnyOrigin();
        builder.AllowAnyHeader();
        builder.AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(builder.Configuration["JwtSettings:SecurityKey"]))
    };
});

builder.Services.AddScoped<IProductsDataService, ProductsDataService>();
builder.Services.AddScoped<IJwtTService, JWTService>();
builder.Services.AddScoped<ICryptographicService, CryptographicService>();
builder.Services.AddScoped<IUserAuthenticationService, UserAuthenticationService>();

DatabaseXMLSchemeParser parser = new DatabaseXMLSchemeParser();
parser.Process();

var app = builder.Build();

app.UseCors();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();

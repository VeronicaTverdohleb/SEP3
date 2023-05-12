using System.Text;
using System.Text.Json.Serialization;
using Application.DaoInterfaces;
using Application.Logic;
using Application.LogicInterfaces;
using EfcDataAccess;
using EfcDataAccess.DAOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;
using WebAPI.Services;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options=>options.JsonSerializerOptions.ReferenceHandler=ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>();

builder.Services.AddScoped<IMenuLogic, MenuLogic>();

builder.Services.AddScoped<IMenuDao, MenuDao>();
builder.Services.AddScoped<IUserDao, UserDao>();
builder.Services.AddScoped<IItemDao, ItemDao>();
builder.Services.AddScoped<IOrderDao, OrderDao>();
builder.Services.AddScoped<IIngredientDao, IngredientDao>();
builder.Services.AddScoped<IIngredientLogic, IngredientLogic>();
builder.Services.AddScoped<ISupplyOrderDao, SupplyOrderDao>();
builder.Services.AddScoped<ISupplyOrderLogic, SupplyOrderLogic>();
builder.Services.AddScoped<IOrderLogic, OrderLogic>();
//builder.Services.AddScoped<IMenuLogic, MenuLogic>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IItemLogic, ItemLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
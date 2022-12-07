using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using PgAPI;
using PgAPI.Services;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Host.ConfigureAppConfiguration((hostingContext, config) =>{
    config.SetBasePath(hostingContext.HostingEnvironment.ContentRootPath);
    config.AddJsonFile("appSettings.json", optional: false, reloadOnChange: false);
    config.AddEnvironmentVariables();
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opt =>
{
    opt.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "JWT Token (you need to put bearer before the token)",
        Type = SecuritySchemeType.ApiKey
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
    opt.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<PGApiContext>(option =>
        option.UseNpgsql(builder.Configuration.GetConnectionString("PgAPIRepository"))
    );
//builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

builder.Services.AddScoped<IUserService, UserService>();

var MyAllowSpecificOrigins = "Cors Policy";

builder.Services.AddCors(opt =>
            {
                opt.AddPolicy(MyAllowSpecificOrigins, policy =>
                {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(new string[] { "http://localhost:3000", "https://svetrivel.github.io" });
                });
            });

var app = builder.Build();

app.UseCors(MyAllowSpecificOrigins);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

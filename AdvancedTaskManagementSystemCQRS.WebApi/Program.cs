using AdvancedTaskManagementSystemCQRS.Infrastructure.DependencyInjection;
using AdvancedTaskManagementSystemCQRS.Application.DependencyInjection;
using Microsoft.OpenApi.Models;
using AdvancedTaskManagementSystemCQRS.WebApi.DependencyInjection;
using AdvancedTaskManagementSystemCQRS.Infrastructure.Configuration;
using AdvancedTaskManagementSystemCQRS.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });
    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.Configure<AuthenticationSettings>(builder.Configuration.GetSection("Authentication"));

builder.Services.AddApplicationMapper();
builder.Services.AddWebApiMapper();

builder.Services.AddTaskManagerDb(builder.Configuration);
builder.Services.AddMediator();
builder.Services.AddInfrastructureMediator();

builder.Services.AddAuthenticationJwtBearer(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<ValidationExceptionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

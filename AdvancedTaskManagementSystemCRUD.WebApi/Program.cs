using AdvancedTaskManagementSystemCRUD.WebApi.Validators;
using AdvancedTaskManagementSystemCRUD.Application.DependencyInjection;
using AdvancedTaskManagementSystemCRUD.Application.Profiles;
using AdvancedTaskManagementSystemCRUD.Infrastructure.Configuration;
using AdvancedTaskManagementSystemCRUD.Infrastructure.DependencyInjection;
using AdvancedTaskManagementSystemCRUD.WebApi.Account;
using FluentValidation;
using Microsoft.OpenApi.Models;

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

builder.Services.AddAutoMapper(cfg =>
{
    cfg.AddMaps(typeof(AccountProfileWebApi).Assembly);
    cfg.AddMaps(typeof(AccountProfileApplication).Assembly);
});

builder.Services.AddTaskManagerDb(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddFluentValidators();
builder.Services.AddValidatorsFromAssembly(typeof(UpdateGroupRequestValidator).Assembly);

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
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1"));
}

app.UseHttpsRedirection();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();

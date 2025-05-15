using LawyerAssistant.Application.Contracts.Common;
using LawyerAssistant.Application.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpClient();
#region Swagger 
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1",
    new OpenApiInfo()
    {
        Title = "Sibaneh API Service V1",
        Version = "v1.0",
        Description = "This The documentation for Sibaneh Web Services"
    });
    options.SwaggerDoc("v2",
    new OpenApiInfo()
    {
        Title = "Sibaneh API Service V2",
        Version = "v2.0",
        Description = "This The documentation for Sibaneh Web Services"
    });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds((type) => type.FullName);
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Description = "Token must appear in header",
        Type = SecuritySchemeType.Http,
        BearerFormat = "Jwt",
        In = ParameterLocation.Header,
        Scheme = "bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "bearer"
                }
            },
            new List<string>()
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


#endregion

#region Base Services and Layers Implementations
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));
builder.Services.RegisterAllDependencyInjection<IScoped>();
builder.Services.RegisterAllDependencyInjection<ISingleton>();
builder.Services.RegisterAllDependencyInjection<ITransient>();
builder.Services.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.AddHttpContextAccessor();
#endregion

#region Authentication Implementation
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver();
});
builder.Services.AddCors();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ClockSkew = TimeSpan.Zero,
        // RequireExpirationTime = true,
        RequireSignedTokens = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(/*""*/  builder.Configuration.GetSection("AppConfig:jwtTokenKey").Value     /*AppConfigProvider.jwtTokenKey()*/)),
        ValidateIssuer = false,
        //ValidIssuer = "localhost:9431",
        ValidateAudience = false,
        //ValidAudience = "localhost:9431",
        ValidateLifetime = false,

    };
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

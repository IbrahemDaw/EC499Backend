Log.Logger = new LoggerConfiguration()
                  .WriteTo.Console()
                  .CreateLogger();
Log.Information("Starting web application");

var builder = WebApplication.CreateBuilder(args);

// Configure logging
builder.Host.AddLogging();

builder.AddServiceDefaults<UserManagementContext>();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter());
    options.Filters.Add(typeof(ValidationFilter));
});

// Model validation
ValidatorOptions.Global.DefaultRuleLevelCascadeMode = CascadeMode.Stop;
ValidatorOptions.Global.DefaultClassLevelCascadeMode = CascadeMode.Stop;
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

var jwtSettings = new JwtOptions();
builder.Configuration.GetSection("JWT").Bind(jwtSettings);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.ValidIssuer,
        ValidAudience = jwtSettings.ValidAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
        ClockSkew = TimeSpan.FromSeconds(jwtSettings.LifeSpan),
    };
});



builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<JwtTokenUtility>();
builder.Services.AddJwtOptions(builder.Configuration);
builder.Services.AddScoped<IUserRepo, UserRepo>();
builder.Services.AddScoped<IRoleRepo, RoleRepo>();
builder.Services.AddEndpointsApiExplorer();
AddSwaggerDoc(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(c => c.AllowAnyHeader()
.AllowAnyOrigin()
.AllowAnyMethod());

app.UseHttpsRedirection();

app.MapDefaultEndpoints();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

static void AddSwaggerDoc(IServiceCollection services)
{
    services.AddSwaggerGen(c =>
    {
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = @"JWT Authorization header using the Bearer scheme. 
                      Enter your token in the text input below.
                      ",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT"
        });

        OpenApiSecurityRequirement securityRequirement = new()
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
                    Array.Empty<string>()
                }
      };

        c.AddSecurityRequirement(securityRequirement);

        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Management Api",
        });
    });
}

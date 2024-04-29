var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServiceDefaults<DMSDbContext>();
builder.Services.AddScoped<ITagRepo,TagRepo>();
builder.Services.AddScoped<IDocumentRepo,DocumentRepo>();
builder.Services.AddScoped<ICategoryRepo,CategoryRepo>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

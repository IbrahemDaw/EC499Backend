
internal partial class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        // Add services to the container.
        // Add services to the container.
        var connStr = "server=localhost;port=3306;user=Ibrahem;password=123456;database=Documetns;Convert Zero Datetime=True;";
        builder.Services.AddDbContext<DMSDbContext>(opt => opt.UseMySQL(
            connStr
          ));
        builder.Services
            .AddRepositories()
            .AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwagger();
        var app = builder.Build();

        // Configure the HTTP request pipeline.

        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors(builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
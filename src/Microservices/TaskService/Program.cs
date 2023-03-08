using TaskService.Implement.NewsCollector;
using TaskService.Tasks;

var builder = Host.CreateApplicationBuilder();
var services = builder.Services;
var configuration = builder.Configuration;

string? connectionString = configuration.GetConnectionString("Default");
services.AddDbContextPool<QueryDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});
services.AddDbContextPool<CommandDbContext>(option =>
{
    _ = option.UseNpgsql(connectionString, sql =>
    {
        _ = sql.MigrationsAssembly("Http.API");
        _ = sql.CommandTimeout(10);
    });
});



services.AddScoped<NewsCollector>();
services.AddHostedService<NewsCollectTask>();

var app = builder.Build();

using (app)
{
    app.Start();
    app.WaitForShutdown();
}

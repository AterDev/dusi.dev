using TaskService.Implement.NewsCollector;
namespace TaskService.Tasks;

/// <summary>
/// 新闻定时采集任务
/// </summary>
public class NewsCollectTask(ILogger<NewsCollectTask> logger, IServiceProvider services) : BackgroundService
{
    private readonly ILogger<NewsCollectTask> _logger = logger;
    private Timer? _timer;
    private readonly IServiceProvider Services = services;

    public override async Task StartAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("News collection service start.");
        await ExecuteAsync(stoppingToken);
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _timer = new Timer(DoWork, stoppingToken, TimeSpan.FromSeconds(10), TimeSpan.FromHours(2));
        return Task.CompletedTask;
    }

    private async void DoWork(object? state)
    {
        using IServiceScope scope = Services.CreateScope();
        NewsCollector newsService = scope.ServiceProvider.GetRequiredService<NewsCollector>();
        await newsService.Start();
    }

    public override Task StopAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Timed Hosted Service is stopping.");
        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public override void Dispose() => _timer?.Dispose();


}

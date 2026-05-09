namespace WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while(!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Arka plan işçisi çalışıyor: {time}", DateTimeOffset.Now);

                //5 saniyede bir log basalım

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}

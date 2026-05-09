using WorkerService;

var builder = Host.CreateApplicationBuilder(args);

// Bu satır projeyi API olmaktan çıkarıp arka plan işçisi yapar
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
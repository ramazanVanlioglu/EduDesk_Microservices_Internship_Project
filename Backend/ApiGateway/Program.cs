using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// Yarp Reverse Proxy Ayarları
builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger Yapılandırması: Diğer servislerin uçlarını buraya tanımlıyoruz
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "EduDesk Gateway API", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // Kendi servislerini Gateway üzerinden Swagger'da göster
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Gateway API");

        // Diğer mikroservislerin Swagger JSON'larını Gateway üzerinden proxy et
        // Not: Bu kısımlar appsettings.json'daki route'lar ile uyumlu olmalı
        c.SwaggerEndpoint("/api/auth/swagger/v1/swagger.json", "Identity Service");
        c.SwaggerEndpoint("/api/lessons/swagger/v1/swagger.json", "Learning Service");
        c.SwaggerEndpoint("/api/tickets/swagger/v1/swagger.json", "Support Service");
    });
}

app.UseAuthorization();
app.MapReverseProxy();
app.MapControllers();

app.Run();

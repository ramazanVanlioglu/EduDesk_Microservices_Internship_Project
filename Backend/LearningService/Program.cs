using Microsoft.EntityFrameworkCore;
using LearningService.Data;
using LearningService.Services;

var builder = WebApplication.CreateBuilder(args);

// Controller desteği
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Veritabanı Bağlantısı (LearningDB için ayrı veritabanı!)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddScoped<IHMACService, HMACService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
   // app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();

app.Run();
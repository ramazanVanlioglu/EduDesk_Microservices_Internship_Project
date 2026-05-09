using Microsoft.EntityFrameworkCore;
using SupportService.Data;

var builder = WebApplication.CreateBuilder(args);


//controller'ları sisteme ekleme
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//veri tabanı bağlantısı => json'dan çekilir
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c =>
    {
        c.RouteTemplate = "swagger/{documentName}/swagger.json";
    });
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.MapControllers();
app.Run();

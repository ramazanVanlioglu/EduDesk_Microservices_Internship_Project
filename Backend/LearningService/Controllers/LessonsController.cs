using Microsoft.AspNetCore.Mvc;
using LearningService.Data;
using LearningService.Entities;
using LearningService.Services;
using Microsoft.EntityFrameworkCore;

namespace LearningService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LessonsController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IHMACService _hmacService;

    public LessonsController(AppDbContext context, IHMACService hmacService)
    {
        _context = context;
        _hmacService = hmacService;
    }


    //test verisi için ders ekleme
    [HttpPost]
    public async Task<IActionResult> CreateLesson(Lesson lesson)
    {
        _context.Lessons.Add(lesson);
        await _context.SaveChangesAsync();

        return Ok(lesson);
    }

    //dersleri listelme
    [HttpGet]
    public async Task<IActionResult> GetLessons()
    {
        var lessons = await _context.Lessons.ToListAsync();

        return Ok(lessons);
    }

    //hmac(hash-based message authentication code) imzalı bağlantı oluşturma
    [HttpGet("generate-join-link/{lessonId}")]
    public IActionResult GenerateJoinLink(int lessonId)
    {
        //örnek kullanıcı id'si => normalde json web token'dan almalıyız
        string userId = "user123";

        string message = "lesson=" + lessonId + "&user=" + userId;

        //imza oluşturma
        string signature = _hmacService.CreateSignature(message);

        string secureUrl = $"/api/lessons/join/{lessonId}?user={userId}&sig={signature}";

        return Ok(new { url = secureUrl });
    }

    //Bağlantının geçerli olup olmadğının kontrolü
    [HttpGet("join/{lessonId}")]
    public IActionResult JoinLesson(int lessonId, [FromQuery] string user, [FromQuery] string sig)
    {
        //gelen bilgilerle meajı tekrar oluştur
        string message = $"lesson={lessonId}&user={user}";

        //imzayı doğrula
        bool isValid = _hmacService.VerifySignature(message, sig);

        if (!isValid)
        {
            return BadRequest("Geçersiz veya sahte bağlantı! Derse erişim yetkiniz yok.");

            
        }
        return Ok(new { message = $"{lessonId} numarılı derse giriş yapıldı." });
    }

}

//kullanıcının bilet oluşturabileceği api.
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupportService.Data;
using SupportService.Entities;

namespace SupportService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TicketsController : ControllerBase
{
    private readonly AppDbContext _context;

    public TicketsController(AppDbContext context)
    {
        _context = context;
    }

    //yeni bilet oluştur
    [HttpPost]
    public async Task<IActionResult> CreateTicket(Ticket ticket)
    {
        _context.Tickets.Add(ticket);
        await _context.SaveChangesAsync();

        return Ok(new { message = "Destek bileti oluşturuldu.", ticketId = ticket.Id });

    }

    //tüm biletleri listele
    [HttpGet]
    public async Task<IActionResult> GetTickets()
    {
        var tickets = await _context.Tickets.ToListAsync();
        return Ok(tickets);
    }
}


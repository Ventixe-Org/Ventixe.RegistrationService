using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ventixe.RegistrationService.Data;
using Ventixe.RegistrationService.Models;

namespace Ventixe.RegistrationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly AppDbContext _db;

        public RegistrationsController(AppDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Registration>>> GetAll([FromQuery] int eventId)
        {
            var list = await _db.Registrations
                .Where(r => r.EventId == eventId)
                .OrderByDescending(r => r.Created)
                .ToListAsync();

            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<Registration>> Create([FromBody] Registration reg)
        {
            _db.Registrations.Add(reg);
            await _db.SaveChangesAsync();
            return CreatedAtAction(nameof(GetAll), new { eventId = reg.EventId }, reg);
        }
    }
}

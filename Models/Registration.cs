using System;

namespace Ventixe.RegistrationService.Models
{
    public class Registration
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public DateTime Created { get; set; } = DateTime.UtcNow;
    }
}

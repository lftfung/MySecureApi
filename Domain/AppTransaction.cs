using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class AppTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public  class AppTransaction
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Guid UserId { get; set; }
        public User user { get; set; }
    }
}

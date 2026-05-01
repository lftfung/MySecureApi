using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MySecureApi.Application.DTOs
{
    public class UpdateTransactionDto
    {
        public decimal Amount { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}

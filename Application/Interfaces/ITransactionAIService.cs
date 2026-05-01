using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySecureApi.Application.Interfaces
{
    public interface ITransactionAIService
    {
        Task<string> PredictCategoryAsync(string description);
    }
}

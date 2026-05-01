using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySecureApi.Application.Interfaces;

namespace MySecureApi.Infrastructure
{
    public class MockAIService : ITransactionAIService
    {
        public Task<string> PredictCategoryAsync(string description) { 
        
            var desc = description.ToLower();

            if (desc.Contains("lunch") || desc.Contains("set-b")) {
                return Task.FromResult("Food");
            
            }
            if (desc.Contains("aseprite")) {
                return Task.FromResult("Software");
            }
            if (desc.Contains("salary")) {
                return Task.FromResult("Income");
            }

            return Task.FromResult("General");
        }

    }
}

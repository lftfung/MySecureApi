using System.Runtime.CompilerServices;
using System.Security.Claims;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Services;
using MySecureApi.Application.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MySecureApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService) {
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("User ID not found");

            var result = await _transactionService.Create(dto, userId);
            return Ok(new {message = "Transaction created successfully", data = result});
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTransactions()
        {

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("User ID nt Found");
            var list = await _transactionService.GetAllByUserId(userId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id) {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("User ID nt Found");

            var transaction = await _transactionService.GetById(id, userId);

            if (transaction == null) {
                return NotFound("Not Found Data");


            }

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("User ID not found");

            var success = await _transactionService.Delete(id, userId);

            if (!success) return NotFound("Not found transaction or no right to delete");

            return Ok(new { message = "Delete success" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody]UpdateTransactionDto dto) {

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized("User ID not found");

            var success = await _transactionService.Update(id, userId, dto);

            if (!success) return NotFound("Update failed, not found or no right");

            return Ok(new { message = "Update success" });
        
        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdString, out var userId) ? userId : Guid.Empty;
        }


    }
}

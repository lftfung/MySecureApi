using System.Runtime.CompilerServices;
using System.Security.Claims;
using Application.DTOs;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controller
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
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString)) {
                return Unauthorized("unfind userid");
            }

            var userid = Guid.Parse(userIdString);

            var result = await _transactionService.Create(dto, userid);
            return Ok(new { message = "record success ", data = result });
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTransactions()
        {

            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var list = await _transactionService.GetAllByUserId(userId);
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id) {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var transaction = await _transactionService.GetById(id, userId);

            if (transaction == null) {
                return NotFound("Not Found Data");


            }

            return Ok(transaction);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var success = await _transactionService.Delete(id, userId);

            if (!success) return NotFound("Not found transaction or no right to delete");

            return Ok(new { message = "Delete success" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateTransactionDto dto) {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var success = await _transactionService.Update(id, userId, dto);

            if (!success) return NotFound("Update failed, not found or no right");

            return Ok(new { message = "Update success" });
        
        }


        [HttpGet("error-test")]
        public IActionResult TestError()
        {
            throw new Exception("error");
        }
    }
}

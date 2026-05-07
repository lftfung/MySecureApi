using System.Security.Claims;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Services;
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
            if (userId == Guid.Empty) return Unauthorized(UserIdNotFound());

            var result = await _transactionService.Create(dto, userId);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTransactions()
        {

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized(UserIdNotFound());
            var result = await _transactionService.GetAllByUserId(userId);
            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id) {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized(UserIdNotFound());

            var result = await _transactionService.GetById(id, userId);

            return result.Success ? Ok(result) : NotFound(result);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id) {
            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized(UserIdNotFound());

            var result = await _transactionService.Delete(id, userId);

            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionDto dto) {

            var userId = GetUserId();
            if (userId == Guid.Empty) return Unauthorized(UserIdNotFound());

            var result = await _transactionService.Update(id, userId, dto);

            return result.Success ? Ok(result) : NotFound(result);

        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdString, out var userId) ? userId : Guid.Empty;
        }

        private ApiResponse<string> UserIdNotFound() {

            return ApiResponse<string>.ErrorResponse("User ID not found");
        }
    }
}

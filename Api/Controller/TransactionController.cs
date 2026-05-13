using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySecureApi.Application.Commands;
using MySecureApi.Application.DTOs;
using MySecureApi.Application.Queries;

namespace MySecureApi.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTransactionDto dto)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized(UserIdNotFound());

            var command = new CreateTransactionCommand(dto, userId);
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetMyTransactions()
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized(UserIdNotFound());

            var query = new GetMyTransactionsQuery(userId);
            var result = await _mediator.Send(query);

            return result.Success ? Ok(result) : BadRequest(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetDetail(Guid id)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized(UserIdNotFound());

            var query = new GetTransactionByIdQuery(id, userId);
            var result = await _mediator.Send(query);

            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized(UserIdNotFound());

            var command = new DeleteTransactionCommand(id, userId);
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : NotFound(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionDto dto)
        {
            var userId = GetUserId();
            if (userId == Guid.Empty)
                return Unauthorized(UserIdNotFound());

            var command = new UpdateTransactionCommand(id, dto, userId);
            var result = await _mediator.Send(command);

            return result.Success ? Ok(result) : NotFound(result);
        }

        private Guid GetUserId()
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(userIdString, out var userId) ? userId : Guid.Empty;
        }

        private ApiResponse<string> UserIdNotFound()
        {
            return ApiResponse<string>.ErrorResponse("User ID not found");
        }
    }
}
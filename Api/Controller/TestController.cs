namespace Api.Controller
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class TestController : ControllerBase
    {
        [Authorize] 
        [HttpGet("secret")]
        public IActionResult GetSecretData()
        {
            return Ok(new { message = "login Secret Data" });
        }
    }
}

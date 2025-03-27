using Microsoft.AspNetCore.Mvc;

namespace Bteste.Controllers{

    [ApiController]
    [Route("")]
public class HomeController:ControllerBase{
    [HttpGet("")]
    public IActionResult Get(){
        return Ok();
    }
}
}
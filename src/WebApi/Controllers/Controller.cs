using Microsoft.AspNetCore.Mvc; 


namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class Controller : ControllerBase
{
    
}
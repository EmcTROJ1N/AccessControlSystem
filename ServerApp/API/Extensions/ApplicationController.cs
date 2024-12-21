using Microsoft.AspNetCore.Mvc;

namespace API.Extensions;

[ApiController]
[Route("api/[controller]")]
public class ApplicationController : ControllerBase
{
    public override OkObjectResult Ok(object? value)
    {
        var envelope = Envelope.Ok(value);

        return base.Ok(envelope);
    }
}
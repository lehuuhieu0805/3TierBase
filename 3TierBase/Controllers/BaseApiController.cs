using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace _3TierBase.API.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/[controller]s")]
    public class BaseApiController : ControllerBase
    {
    }
}

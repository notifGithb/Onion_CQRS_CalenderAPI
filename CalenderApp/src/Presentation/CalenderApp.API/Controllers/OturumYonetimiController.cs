using CalenderApp.Application.Features.OturumYonetimi.Commands.GirisYap;
using CalenderApp.Application.Features.OturumYonetimi.Commands.KayitOl;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalenderApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class OturumYonetimiController : ControllerBase
    {
        private readonly IMediator _mediator;
        public OturumYonetimiController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost]
        public async Task<IActionResult> KayitOl([FromBody] KayitOlRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }


        [HttpPost]
        public async Task<IActionResult> GirisYap([FromBody] GirisYapRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);
            return Ok(response);

        }
    }
}

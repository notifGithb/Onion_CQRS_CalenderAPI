using CalenderApp.Application.Features.Kullanicilar.Queries.MevcutKullaniciGetir;
using CalenderApp.Application.Features.Kullanicilar.Queries.TumKullanicilariGetir;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CalenderApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class KullaniciController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KullaniciController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> MevcutKullaniciGetir()
        {
            var response = await _mediator.Send(new MevcutKullaniciGetirRequest());

            return Ok(response);
        }



        [HttpGet]
        public async Task<IActionResult> TumKullanicilariGetir()
        {
            var response = await _mediator.Send(new TumKullanicilariGetirRequest());

            return Ok(response);
        }
    }
}

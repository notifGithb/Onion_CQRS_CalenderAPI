using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinligeKullaniciEkle;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikGuncelle;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikOlustur;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil;
using CalenderApp.Application.Features.Etkinlikler.Commands.EtkinliktenDavetliKullanicilariSil;
using CalenderApp.Application.Features.Etkinlikler.Queries.EklenenEtkinlikleriGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinlikleriGetir;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CalenderApp.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EtkinlikController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EtkinlikController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> EtkinlikOlustur([FromBody] EtkinlikOlusturRequest request)
        {
            if (request == null)
                return BadRequest();

            await _mediator.Send(request);
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> EtkinlikGuncelle([FromBody] EtkinlikGuncelleRequest request)
        {
            if (request == null)
                return BadRequest();

            await _mediator.Send(request);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> EtkinlikSil([FromQuery] EtkinlikSilRequest request)
        {
            if (request == null)
                return BadRequest();

            await _mediator.Send(request);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> EtkinligeKullaniciEkle([FromBody] EtkinligeKullaniciEkleRequest request)
        {
            if (request == null)
                return BadRequest();

            await _mediator.Send(request);
            return Ok();
        }


        [HttpDelete]
        public async Task<IActionResult> EtkinliktenDavetliKullanicilariSil([FromBody] EtkinliktenDavetliKullanicilariSilRequest request)
        {
            if (request == null)
                return BadRequest();

            await _mediator.Send(request);
            return Ok();
        }


        [HttpGet]
        public async Task<IActionResult> EtkinligeDavetliKullanicilariGetir([FromQuery] int etkinlikId)
        {
            var response = await _mediator.Send(new EtkinligeDavetliKullanicilariGetirRequest { EtkinlikId = etkinlikId });
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciEtkinligiGetir([FromQuery] int etkinlikId)
        {
            var response = await _mediator.Send(new KullaniciEtkinligiGetirRequest { EtkinlikId = etkinlikId });
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciEtkinlikleriGetir()
        {
            var response = await _mediator.Send(new KullaniciEtkinlikleriGetirRequest());
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> EklenenEtkinlikleriGetir()
        {
            var response = await _mediator.Send(new EklenenEtkinlikleriGetirRequest());
            return Ok(response);
        }


        [HttpGet]
        public async Task<IActionResult> KullaniciAylikEtkinlikGetir(DateTime tarih)
        {
            var response = await _mediator.Send(new KullaniciAylikEtkinlikGetirRequest { Tarih = tarih });
            return Ok(response);
        }
    }
}

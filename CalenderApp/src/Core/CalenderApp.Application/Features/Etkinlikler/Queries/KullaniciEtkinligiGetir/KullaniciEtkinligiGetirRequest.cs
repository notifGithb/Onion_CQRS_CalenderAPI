using MediatR;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirRequest : IRequest<KullaniciEtkinligiGetirResponse>
    {
        public required int EtkinlikId { get; set; }
    }
}

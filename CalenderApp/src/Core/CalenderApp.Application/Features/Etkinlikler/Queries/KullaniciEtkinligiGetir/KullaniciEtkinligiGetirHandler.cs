using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirHandler : BaseHandler, IRequestHandler<KullaniciEtkinligiGetirRequest, KullaniciEtkinligiGetirResponse>
    {
        public KullaniciEtkinligiGetirHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<KullaniciEtkinligiGetirResponse> Handle(KullaniciEtkinligiGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            Etkinlik? kullaniciEtkinligi = await _calenderAppDbContext.Etkinliks.Where(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId).FirstOrDefaultAsync(cancellationToken);

            if (kullaniciEtkinligi == null) throw new Exception("Kullanici Etkinlikleri Bulunumadı.");

            return _mapper.Map<KullaniciEtkinligiGetirResponse>(kullaniciEtkinligi);

        }
    }
}

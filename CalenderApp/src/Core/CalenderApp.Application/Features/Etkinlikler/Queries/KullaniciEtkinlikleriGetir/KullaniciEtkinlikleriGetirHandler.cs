using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinlikleriGetir
{
    public class KullaniciEtkinlikleriGetirHandler : BaseHandler, IRequestHandler<KullaniciEtkinlikleriGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public KullaniciEtkinlikleriGetirHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciEtkinlikleriGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            IList<Etkinlik> kullaniciEtkinlikleri = await _calenderAppDbContext.Etkinliks.Where(e => e.OlusturanKullaniciId == mevcutKullaniciId).ToListAsync(cancellationToken);

            if (!kullaniciEtkinlikleri.Any()) throw new Exception("Kullanici Etkinliği Bulunamdı.");

            return _mapper.Map<IList<KullaniciEtkinligiGetirResponse>>(kullaniciEtkinlikleri);
        }
    }
}

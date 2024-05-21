using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinlikleriGetir
{
    public class KullaniciEtkinlikleriGetirHandler : BaseHandler, IRequestHandler<KullaniciEtkinlikleriGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public KullaniciEtkinlikleriGetirHandler(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciEtkinlikleriGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            IList<Etkinlik> kullaniciEtkinlikleri = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!kullaniciEtkinlikleri.Any()) throw new Exception("Kullanıcı Etkinliği Bulunamdı.");

            return _mapper.Map<IList<KullaniciEtkinligiGetirResponse>>(kullaniciEtkinlikleri);
        }
    }
}

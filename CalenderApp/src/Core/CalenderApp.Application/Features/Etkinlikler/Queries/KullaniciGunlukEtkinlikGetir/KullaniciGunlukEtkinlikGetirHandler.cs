using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciGunlukEtkinlikGetir
{
    public class KullaniciGunlukEtkinlikGetirHandler : BaseHandler, IRequestHandler<KullaniciGunlukEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public KullaniciGunlukEtkinlikGetirHandler(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciGunlukEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            List<Etkinlik>? deneme = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.DayOfYear == request.Tarih.DayOfYear &&
                            e.BaslangicTarihi.Year == request.Tarih.Year)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!deneme.Any()) throw new Exception("İstenen Güne Ait Kullanıcı Etkinliği Bulunamadı.");

            return _mapper.Map<IList<KullaniciEtkinligiGetirResponse>>(deneme);
        }
    }
}

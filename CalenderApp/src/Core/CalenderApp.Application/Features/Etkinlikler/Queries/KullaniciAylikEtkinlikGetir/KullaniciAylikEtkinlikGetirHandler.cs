using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir
{
    public class KullaniciAylikEtkinlikGetirHandler : BaseHandler, IRequestHandler<KullaniciAylikEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public KullaniciAylikEtkinlikGetirHandler(IMapper mapper, IHttpContextAccessor httpContextAccessor, CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciAylikEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            List<Etkinlik>? deneme = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.Month == request.Tarih.Month &&
                            e.BaslangicTarihi.Year == request.Tarih.Year &&
                            e.BitisTarihi.Month == request.Tarih.Month &&
                            e.BaslangicTarihi.Year == request.Tarih.Year)
                .ToListAsync(cancellationToken);

            if (!deneme.Any()) throw new Exception("İstenen Ay'a Ait Kullanici Etkinliği Bulunamadı.");

            return _mapper.Map<IList<KullaniciEtkinligiGetirResponse>>(deneme);
        }
    }
}

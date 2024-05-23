using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciAylikEtkinlikGetir
{
    public class KullaniciAylikEtkinlikGetirHandler : BaseHandler, IRequestHandler<KullaniciAylikEtkinlikGetirRequest, IList<KullaniciEtkinligiGetirResponse>>
    {
        public KullaniciAylikEtkinlikGetirHandler(
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<KullaniciEtkinligiGetirResponse>> Handle(KullaniciAylikEtkinlikGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            List<Etkinlik>? deneme = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId &&
                            e.BaslangicTarihi.Month == request.Tarih.Month &&
                            e.BaslangicTarihi.Year == request.Tarih.Year)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!deneme.Any()) throw new Exception("İstenen Ay'a Ait Kullanıcı Etkinliği Bulunamadı.");

            return _mapper.Map<IList<KullaniciEtkinligiGetirResponse>>(deneme);
        }
    }
}

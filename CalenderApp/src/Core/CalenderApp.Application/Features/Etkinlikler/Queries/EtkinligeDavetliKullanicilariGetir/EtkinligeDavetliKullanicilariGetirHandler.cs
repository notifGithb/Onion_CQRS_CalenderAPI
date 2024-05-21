using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EtkinligeDavetliKullanicilariGetir
{
    public class EtkinligeDavetliKullanicilariGetirHandler : BaseHandler, IRequestHandler<EtkinligeDavetliKullanicilariGetirRequest, IList<EtkinligeDavetliKullanicilariGetirResponse>>
    {
        public EtkinligeDavetliKullanicilariGetirHandler(
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<EtkinligeDavetliKullanicilariGetirResponse>> Handle(EtkinligeDavetliKullanicilariGetirRequest request, CancellationToken cancellationToken)
        {
            if (!await _calenderAppDbContext.Etkinliks.AnyAsync(e => e.Id == request.EtkinlikId && e.OlusturanKullaniciId == mevcutKullaniciId, cancellationToken)) throw new Exception("Kullanıcını Kayıtlı Etkinliği Bulunamadı.");

            IList<Kullanici> kullanicilar = await _calenderAppDbContext.KullaniciEtkinliks
                .Where(e => e.EtkinlikId == request.EtkinlikId)
                .Select(e => e.Kullanici)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return _mapper.Map<IList<EtkinligeDavetliKullanicilariGetirResponse>>(kullanicilar);
        }
    }
}

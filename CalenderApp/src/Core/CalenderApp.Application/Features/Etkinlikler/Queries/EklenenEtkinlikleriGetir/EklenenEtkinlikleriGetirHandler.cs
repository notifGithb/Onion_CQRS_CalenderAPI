using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.EklenenEtkinlikleriGetir
{
    public class EklenenEtkinlikleriGetirHandler : BaseHandler, IRequestHandler<EklenenEtkinlikleriGetirRequest, IList<EklenenEtkinlikleriGetirResponse>>
    {
        public EklenenEtkinlikleriGetirHandler(
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<IList<EklenenEtkinlikleriGetirResponse>> Handle(EklenenEtkinlikleriGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");


            IList<Etkinlik> eklenenEtkinlikler = await _calenderAppDbContext.KullaniciEtkinliks
                .Where(e => e.KullaniciId == mevcutKullaniciId)
                .Include(e => e.Etkinlik.OlusturanKullanici)
                .Select(e => e.Etkinlik)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            if (!eklenenEtkinlikler.Any()) throw new Exception("Eklenen Etkinlik Bulunamadı");

            return _mapper.Map<IList<EklenenEtkinlikleriGetirResponse>>(eklenenEtkinlikler);
        }
    }
}

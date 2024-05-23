using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Application.Features.Etkinlikler.Queries.Bases;
using CalenderApp.Domain.Entities;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirHandler : BaseHandler, IRequestHandler<KullaniciEtkinligiGetirRequest, KullaniciEtkinligiGetirResponse>
    {
        public KullaniciEtkinligiGetirHandler(
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task<KullaniciEtkinligiGetirResponse> Handle(KullaniciEtkinligiGetirRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanıcı Bulunamadı.");

            Etkinlik? kullaniciEtkinligi = await _calenderAppDbContext.Etkinliks
                .Where(e => e.OlusturanKullaniciId == mevcutKullaniciId && e.Id == request.EtkinlikId)
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);

            if (kullaniciEtkinligi == null) throw new Exception("Kullanıcı Etkinlikleri Bulunumadı.");

            return _mapper.Map<KullaniciEtkinligiGetirResponse>(kullaniciEtkinligi);
        }
    }
}

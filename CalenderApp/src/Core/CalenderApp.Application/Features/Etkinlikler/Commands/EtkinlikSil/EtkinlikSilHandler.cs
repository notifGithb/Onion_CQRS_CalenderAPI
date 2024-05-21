using ActivityCalender.Entities;
using AutoMapper;
using CalenderApp.Application.Bases;
using CalenderApp.Persistence.Context;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CalenderApp.Application.Features.Etkinlikler.Commands.EtkinlikSil
{
    public class EtkinlikSilHandler : BaseHandler, IRequestHandler<EtkinlikSilRequest>
    {
        public EtkinlikSilHandler(
            IMapper mapper, 
            IHttpContextAccessor httpContextAccessor, 
            CalenderAppDbContext calenderAppDbContext) : base(mapper, httpContextAccessor, calenderAppDbContext)
        {
        }

        public async Task Handle(EtkinlikSilRequest request, CancellationToken cancellationToken)
        {
            if (mevcutKullaniciId == null) throw new Exception("Mevcut Kullanici Bulunamadi.");

            Etkinlik? etkinlikSil = await _calenderAppDbContext.Etkinliks.Where(e => e.Id == request.EtkinlikId && e.OlusturanKullaniciId == mevcutKullaniciId).FirstOrDefaultAsync(cancellationToken) ?? throw new Exception("Silinmek İstenen Etkinlik Kaydı Bulunamadı.");

            _calenderAppDbContext.Etkinliks.Remove(etkinlikSil);
            await _calenderAppDbContext.SaveChangesAsync(cancellationToken);
        }
    }
}

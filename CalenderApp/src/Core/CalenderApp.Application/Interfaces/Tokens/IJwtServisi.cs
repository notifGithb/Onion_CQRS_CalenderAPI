using ActivityCalender.Entities;

namespace CalenderApp.Application.Interfaces.Tokens
{
    public interface IJwtServisi
    {
        string JwtTokenOlustur(Kullanici kullanici);

    }
}

﻿using ActivityCalender.Entities;

namespace CalenderApp.Application.Features.Etkinlikler.Queries.KullaniciEtkinligiGetir
{
    public class KullaniciEtkinligiGetirResponse
    {
        public int Id { get; set; }
        public required string Baslik { get; set; }
        public string? Aciklama { get; set; }
        public DateTime BaslangicTarihi { get; set; }
        public DateTime BitisTarihi { get; set; }
        public TekrarEnum TekrarDurumu { get; set; }
    }
}
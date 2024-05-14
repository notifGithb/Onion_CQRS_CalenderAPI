using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CalenderApp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kullanicis",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KullaniciAdi = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Isim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Soyisim = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KullaniciSifresi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kullanicis", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Etkinliks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Baslik = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Aciklama = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BaslangicTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BitisTarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TekrarDurumu = table.Column<int>(type: "int", nullable: false),
                    OlusturanKullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Etkinliks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Etkinliks_Kullanicis_OlusturanKullaniciId",
                        column: x => x.OlusturanKullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "KullaniciEtkinliks",
                columns: table => new
                {
                    EtkinlikId = table.Column<int>(type: "int", nullable: false),
                    KullaniciId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KullaniciEtkinliks", x => new { x.KullaniciId, x.EtkinlikId });
                    table.ForeignKey(
                        name: "FK_KullaniciEtkinliks_Etkinliks_EtkinlikId",
                        column: x => x.EtkinlikId,
                        principalTable: "Etkinliks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_KullaniciEtkinliks_Kullanicis_KullaniciId",
                        column: x => x.KullaniciId,
                        principalTable: "Kullanicis",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Etkinliks_OlusturanKullaniciId",
                table: "Etkinliks",
                column: "OlusturanKullaniciId");

            migrationBuilder.CreateIndex(
                name: "IX_KullaniciEtkinliks_EtkinlikId",
                table: "KullaniciEtkinliks",
                column: "EtkinlikId");

            migrationBuilder.CreateIndex(
                name: "IX_Kullanicis_KullaniciAdi",
                table: "Kullanicis",
                column: "KullaniciAdi",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KullaniciEtkinliks");

            migrationBuilder.DropTable(
                name: "Etkinliks");

            migrationBuilder.DropTable(
                name: "Kullanicis");
        }
    }
}

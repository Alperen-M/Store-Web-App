using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    public partial class AddStoresTableAndStoreIdToProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Stores tablosunu oluştur
            migrationBuilder.CreateTable(
                name: "Stores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                         .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stores", x => x.Id);
                });

            // Products tablosuna StoreId kolonu ekle
            migrationBuilder.AddColumn<int>(
                name: "StoreId",
                table: "Products",
                type: "int",
                nullable: true,
                defaultValue: 0); // Eğer ürünlerin mutlaka mağazası olacaksa nullable false, yoksa true yapabilirsin

            // StoreId için index oluştur
            migrationBuilder.CreateIndex(
                name: "IX_Products_StoreId",
                table: "Products",
                column: "StoreId");

            // Foreign key ilişki kur
            migrationBuilder.AddForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // İlişkiyi kaldır
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Stores_StoreId",
                table: "Products");

            // Indexi kaldır
            migrationBuilder.DropIndex(
                name: "IX_Products_StoreId",
                table: "Products");

            // StoreId kolonunu kaldır
            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Products");

            // Stores tablosunu kaldır
            migrationBuilder.DropTable(
                name: "Stores");
        }
    }
}

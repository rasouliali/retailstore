using Microsoft.EntityFrameworkCore.Migrations;

namespace RetailStorePrj.Data.Migrations
{
    public partial class workstation_retail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Workstations_RetailStoreId",
                table: "Workstations",
                column: "RetailStoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Workstations_RetailStores_RetailStoreId",
                table: "Workstations",
                column: "RetailStoreId",
                principalTable: "RetailStores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Workstations_RetailStores_RetailStoreId",
                table: "Workstations");

            migrationBuilder.DropIndex(
                name: "IX_Workstations_RetailStoreId",
                table: "Workstations");
        }
    }
}

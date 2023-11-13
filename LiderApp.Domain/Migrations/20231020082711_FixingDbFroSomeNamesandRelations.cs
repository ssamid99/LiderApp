using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LiderApp.Domain.Migrations
{
    public partial class FixingDbFroSomeNamesandRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_MenuId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Services_RepairOrders_RepairOrderId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Services_RepairOrderId",
                table: "Services");

            migrationBuilder.DropIndex(
                name: "IX_Menus_MenuId",
                table: "Menus");

            migrationBuilder.DropColumn(
                name: "RepairOrderId",
                table: "Services");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Menus");

            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "RepairOrders",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "Menus",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RepairOrders_ServiceId",
                table: "RepairOrders",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_ParentId",
                table: "Menus",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus",
                column: "ParentId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RepairOrders_Services_ServiceId",
                table: "RepairOrders",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Menus_ParentId",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_RepairOrders_Services_ServiceId",
                table: "RepairOrders");

            migrationBuilder.DropIndex(
                name: "IX_RepairOrders_ServiceId",
                table: "RepairOrders");

            migrationBuilder.DropIndex(
                name: "IX_Menus_ParentId",
                table: "Menus");

            migrationBuilder.AddColumn<string>(
                name: "RepairOrderId",
                table: "Services",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ServiceId",
                table: "RepairOrders",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ParentId",
                table: "Menus",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MenuId",
                table: "Menus",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Services_RepairOrderId",
                table: "Services",
                column: "RepairOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_MenuId",
                table: "Menus",
                column: "MenuId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Menus_MenuId",
                table: "Menus",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Services_RepairOrders_RepairOrderId",
                table: "Services",
                column: "RepairOrderId",
                principalTable: "RepairOrders",
                principalColumn: "Id");
        }
    }
}

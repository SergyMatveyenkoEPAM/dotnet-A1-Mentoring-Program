using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFNorthwind.Migrations
{
    public partial class Northwind_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Region",
                table: "Territories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.AddColumn<DateTime>(
                name: "EstablishmentDate",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "RegionID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Region",
                table: "Territories",
                column: "RegionID",
                principalTable: "Regions",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Region",
                table: "Territories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropColumn(
                name: "EstablishmentDate",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "RegionID")
                .Annotation("SqlServer:Clustered", false);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Region",
                table: "Territories",
                column: "RegionID",
                principalTable: "Region",
                principalColumn: "RegionID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

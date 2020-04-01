using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EFNorthwind.Migrations
{
    public partial class Northwind_13 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "EstablishmentDate",
                table: "Customers",
                nullable: true);
            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EstablishmentDate",
                table: "Customers");
        }
    }
}

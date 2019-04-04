using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Proyecto_Final.Migrations
{
    public partial class AddMaintenanceModels3 : Migration
    {
        //MIGRATION ONLY FOR INCREMENT MAX LENGHT OF POSITION'S NAME FIELD
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PositionName",
                table: "Positions",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Employees",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 8);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PositionName",
                table: "Positions",
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 35);

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Employees",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(int),
                oldMaxLength: 8);
        }
    }
}

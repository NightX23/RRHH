using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Proyecto_Final.Migrations
{
    public partial class AddMaintenanceModels4 : Migration
    {
        //MIGRATION ONLY FOR INCREMENT MAX LENGHT OF POSITION'S NAME FIELD
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PositionName",
                table: "Positions",
                maxLength: 40,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 35);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "PositionName",
                table: "Positions",
                maxLength: 35,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 40);
        }
    }
}

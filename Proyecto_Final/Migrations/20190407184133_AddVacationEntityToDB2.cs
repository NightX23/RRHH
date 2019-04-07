using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Proyecto_Final.Migrations
{
    public partial class AddVacationEntityToDB2 : Migration
    {

        //FIXING NAME OF COMMENT FIELD
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "comment",
                table: "Vacations",
                newName: "Comment");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Vacations",
                newName: "comment");
        }
    }
}

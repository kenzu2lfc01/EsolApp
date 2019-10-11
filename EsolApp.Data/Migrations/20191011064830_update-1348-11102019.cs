using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EsolApp.Data.Migrations
{
    public partial class update134811102019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateBy",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifyBy",
                table: "Todos",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifyDate",
                table: "Todos",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ModifyBy",
                table: "Todos");

            migrationBuilder.DropColumn(
                name: "ModifyDate",
                table: "Todos");
        }
    }
}

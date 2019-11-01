using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EsolApp.Data.Migrations
{
    public partial class update131239102019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Todos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "TodoShare",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyDate = table.Column<DateTime>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    TodosId = table.Column<int>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoShare", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoShare_Todos_TodosId",
                        column: x => x.TodosId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoShare_TodosId",
                table: "TodoShare",
                column: "TodosId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoShare");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Todos");
        }
    }
}

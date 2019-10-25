using Microsoft.EntityFrameworkCore.Migrations;

namespace EsolApp.Data.Migrations
{
    public partial class update135425102019 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Todos_TodosId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TodosId",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "TodosId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "TodoId",
                table: "Images",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Images_TodoId",
                table: "Images",
                column: "TodoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Todos_TodoId",
                table: "Images",
                column: "TodoId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Todos_TodoId",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_TodoId",
                table: "Images");

            migrationBuilder.AlterColumn<int>(
                name: "TodoId",
                table: "Images",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TodosId",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Images_TodosId",
                table: "Images",
                column: "TodosId");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Todos_TodosId",
                table: "Images",
                column: "TodosId",
                principalTable: "Todos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class addcomment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "comment",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "CommentOfBook",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentOfBook",
                table: "Comments");

            migrationBuilder.AddColumn<string>(
                name: "comment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

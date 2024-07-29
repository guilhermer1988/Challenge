using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    public partial class updateHistory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId",
                table: "TaskHistory",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_CommentId",
                table: "TaskHistory",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskHistory_Comment_CommentId",
                table: "TaskHistory",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskHistory_Comment_CommentId",
                table: "TaskHistory");

            migrationBuilder.DropIndex(
                name: "IX_TaskHistory_CommentId",
                table: "TaskHistory");

            migrationBuilder.DropColumn(
                name: "CommentId",
                table: "TaskHistory");
        }
    }
}

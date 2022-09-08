using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class mig4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GitHubProfiles_Developers_UserId",
                table: "GitHubProfiles");

            migrationBuilder.DropIndex(
                name: "IX_GitHubProfiles_UserId",
                table: "GitHubProfiles");

            migrationBuilder.AddColumn<int>(
                name: "DeveloperId",
                table: "GitHubProfiles",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_GitHubProfiles_DeveloperId",
                table: "GitHubProfiles",
                column: "DeveloperId");

            migrationBuilder.AddForeignKey(
                name: "FK_GitHubProfiles_Developers_DeveloperId",
                table: "GitHubProfiles",
                column: "DeveloperId",
                principalTable: "Developers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GitHubProfiles_Developers_DeveloperId",
                table: "GitHubProfiles");

            migrationBuilder.DropIndex(
                name: "IX_GitHubProfiles_DeveloperId",
                table: "GitHubProfiles");

            migrationBuilder.DropColumn(
                name: "DeveloperId",
                table: "GitHubProfiles");

            migrationBuilder.CreateIndex(
                name: "IX_GitHubProfiles_UserId",
                table: "GitHubProfiles",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_GitHubProfiles_Developers_UserId",
                table: "GitHubProfiles",
                column: "UserId",
                principalTable: "Developers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

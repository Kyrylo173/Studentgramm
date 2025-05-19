using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentgram.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LikedUsers",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LikedUsers",
                table: "Photos");
        }
    }
}

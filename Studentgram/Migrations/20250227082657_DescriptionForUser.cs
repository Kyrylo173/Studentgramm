using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Studentgram.Migrations
{
    /// <inheritdoc />
    public partial class DescriptionForUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "User",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "User");
        }
    }
}

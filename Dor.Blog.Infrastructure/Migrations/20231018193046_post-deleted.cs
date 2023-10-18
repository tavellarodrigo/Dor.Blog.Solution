using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Dor.Blog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class postdeleted : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Deleted",
                table: "BlogPosts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Deleted",
                table: "BlogPosts");
        }
    }
}

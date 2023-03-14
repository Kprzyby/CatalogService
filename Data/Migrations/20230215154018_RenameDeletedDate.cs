using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PlatformService.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameDeletedDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DelatedDate",
                table: "Platforms",
                newName: "DeletedDate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedDate",
                table: "Platforms",
                newName: "DelatedDate");
        }
    }
}
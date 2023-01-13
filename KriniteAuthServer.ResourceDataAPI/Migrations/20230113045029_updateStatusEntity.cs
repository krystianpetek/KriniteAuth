using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KriniteAuthServer.ResourceDataAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateStatusEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "State",
                table: "Complaints",
                newName: "Status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Complaints",
                newName: "State");
        }
    }
}

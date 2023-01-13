using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KriniteAuthServer.ResourceDataAPI.Migrations
{
    /// <inheritdoc />
    public partial class addedApplicant : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ApplicantModel_ApplicantId",
                table: "Complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ApplicantModel",
                table: "ApplicantModel");

            migrationBuilder.RenameTable(
                name: "ApplicantModel",
                newName: "Applicants");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Applicants_ApplicantId",
                table: "Complaints",
                column: "ApplicantId",
                principalTable: "Applicants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Applicants_ApplicantId",
                table: "Complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Applicants",
                table: "Applicants");

            migrationBuilder.RenameTable(
                name: "Applicants",
                newName: "ApplicantModel");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ApplicantModel",
                table: "ApplicantModel",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ApplicantModel_ApplicantId",
                table: "Complaints",
                column: "ApplicantId",
                principalTable: "ApplicantModel",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

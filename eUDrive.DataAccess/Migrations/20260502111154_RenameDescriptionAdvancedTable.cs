using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace eUDrive.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RenameDescriptionAdvancedTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Description_DescriptionAdvacned_DescriptionAdvancedId",
                table: "Description");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DescriptionAdvacned",
                table: "DescriptionAdvacned");

            migrationBuilder.RenameTable(
                name: "DescriptionAdvacned",
                newName: "DescriptionAdvanced");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DescriptionAdvanced",
                table: "DescriptionAdvanced",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Description_DescriptionAdvanced_DescriptionAdvancedId",
                table: "Description",
                column: "DescriptionAdvancedId",
                principalTable: "DescriptionAdvanced",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Description_DescriptionAdvanced_DescriptionAdvancedId",
                table: "Description");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DescriptionAdvanced",
                table: "DescriptionAdvanced");

            migrationBuilder.RenameTable(
                name: "DescriptionAdvanced",
                newName: "DescriptionAdvacned");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DescriptionAdvacned",
                table: "DescriptionAdvacned",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Description_DescriptionAdvacned_DescriptionAdvancedId",
                table: "Description",
                column: "DescriptionAdvancedId",
                principalTable: "DescriptionAdvacned",
                principalColumn: "Id");
        }
    }
}

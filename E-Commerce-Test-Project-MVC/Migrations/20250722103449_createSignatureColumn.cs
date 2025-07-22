using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Test_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class createSignatureColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Signature",
                table: "SliderInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Signature",
                table: "SliderInfos");
        }
    }
}

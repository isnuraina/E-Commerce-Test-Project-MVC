using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Test_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class createMiniTextColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MiniText",
                table: "SliderInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MiniText",
                table: "SliderInfos");
        }
    }
}

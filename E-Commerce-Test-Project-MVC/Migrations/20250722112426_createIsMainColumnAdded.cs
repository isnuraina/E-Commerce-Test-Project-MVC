using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Test_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class createIsMainColumnAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "SliderInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "SliderInfos");
        }
    }
}

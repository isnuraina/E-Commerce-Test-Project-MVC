using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace E_Commerce_Test_Project_MVC.Migrations
{
    /// <inheritdoc />
    public partial class addedIsMainColumnToProductImageTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsMain",
                table: "ProductImages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsMain",
                table: "ProductImages");
        }
    }
}

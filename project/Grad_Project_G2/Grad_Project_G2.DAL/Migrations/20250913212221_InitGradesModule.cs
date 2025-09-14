using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Grad_Project_G2.DAL.Migrations
{
    /// <inheritdoc />
    public partial class InitGradesModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sessions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sessions");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonManager.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddUserCreatorIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserCreatorId",
                table: "Users",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserCreatorId",
                table: "SalonServices",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserCreatorId",
                table: "Customers",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserCreatorId",
                table: "Appointments",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "SalonServices");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "UserCreatorId",
                table: "Appointments");
        }
    }
}

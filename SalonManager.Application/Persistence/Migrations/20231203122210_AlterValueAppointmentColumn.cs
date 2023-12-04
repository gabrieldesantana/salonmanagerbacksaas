using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SalonManager.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AlterValueAppointmentColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Value",
                table: "Appointments",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Value",
                table: "Appointments",
                type: "real",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "numeric");
        }
    }
}

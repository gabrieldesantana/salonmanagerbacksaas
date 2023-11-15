using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace SalonManager.Application.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixServicesField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalonServices_Appointments_Id",
                table: "SalonServices");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SalonServices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceAppointmentId",
                table: "Appointments",
                column: "ServiceAppointmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointments_SalonServices_ServiceAppointmentId",
                table: "Appointments",
                column: "ServiceAppointmentId",
                principalTable: "SalonServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointments_SalonServices_ServiceAppointmentId",
                table: "Appointments");

            migrationBuilder.DropIndex(
                name: "IX_Appointments_ServiceAppointmentId",
                table: "Appointments");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "SalonServices",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddForeignKey(
                name: "FK_SalonServices_Appointments_Id",
                table: "SalonServices",
                column: "Id",
                principalTable: "Appointments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

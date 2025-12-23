using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scs.DbMigration.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class BaseEntitySoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Faculties",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Faculties",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "Departments",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "ClearanceSignatures",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "ClearanceSignatories",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "ClearanceRules",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "ClearanceForms",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Faculties");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "ClearanceSignatures");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "ClearanceSignatories");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "ClearanceRules");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "ClearanceForms");

            migrationBuilder.AlterColumn<Guid>(
                name: "DepartmentId",
                table: "Faculties",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);
        }
    }
}

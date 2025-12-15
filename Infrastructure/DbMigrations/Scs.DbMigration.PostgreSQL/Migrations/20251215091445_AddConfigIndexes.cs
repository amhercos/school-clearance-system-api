using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Scs.DbMigration.PostgreSQL.Migrations
{
    /// <inheritdoc />
    public partial class AddConfigIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Students_StudentProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId1",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_FacultyProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_StudentProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DepartmentId1",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "FacultyProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "StudentProfileId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DepartmentId1",
                table: "Students",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FacultyProfileId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StudentProfileId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId1",
                table: "Students",
                column: "DepartmentId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_FacultyProfileId",
                table: "AspNetUsers",
                column: "FacultyProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_StudentProfileId",
                table: "AspNetUsers",
                column: "StudentProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Faculties_FacultyProfileId",
                table: "AspNetUsers",
                column: "FacultyProfileId",
                principalTable: "Faculties",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Students_StudentProfileId",
                table: "AspNetUsers",
                column: "StudentProfileId",
                principalTable: "Students",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId1",
                table: "Students",
                column: "DepartmentId1",
                principalTable: "Departments",
                principalColumn: "Id");
        }
    }
}

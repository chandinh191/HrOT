using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace hrOT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveEmployeeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BankName",
                table: "Employees"
                );
            migrationBuilder.DropColumn(
                name: "BankAccountNumber",
                table: "Employees"
                );
            migrationBuilder.DropColumn(
                name: "BankAccountName",
                table: "Employees"
                );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankName",
                table: "Employees",
                nullable: false
                );
            migrationBuilder.AddColumn<string>(
                name: "BankAccountNumber",
                table: "Employees",
                nullable: false
                );
            migrationBuilder.AddColumn<string>(
                name: "BankAccountName",
                table: "Employees",
                nullable: false
                );
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fluentify.Database.Migrations
{
    /// <inheritdoc />
    public partial class changedusert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Users");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RegDate",
                table: "Users",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RegDate",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "PhoneNumber",
                table: "Users",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}

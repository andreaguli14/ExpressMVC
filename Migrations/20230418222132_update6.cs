using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgenziaSpedizioni.Migrations
{
    /// <inheritdoc />
    public partial class update6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<int>>(
                name: "Shipments",
                table: "Users",
                type: "integer[]",
                nullable: true,
                oldClrType: typeof(List<string>),
                oldType: "text[]",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<List<string>>(
                name: "Shipments",
                table: "Users",
                type: "text[]",
                nullable: true,
                oldClrType: typeof(List<int>),
                oldType: "integer[]",
                oldNullable: true);
        }
    }
}

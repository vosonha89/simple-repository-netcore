using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevNetCore.SimpleRepositoryExample.Migrations
{
    /// <inheritdoc />
    public partial class createtodotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ToDo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    createdDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    lastUpdateBy = table.Column<string>(type: "text", nullable: true),
                    lastUpdateDate = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    completed = table.Column<bool>(type: "boolean", nullable: false),
                    userId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDo", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ToDo");
        }
    }
}

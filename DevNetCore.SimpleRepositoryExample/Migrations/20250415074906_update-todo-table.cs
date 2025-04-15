using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevNetCore.SimpleRepositoryExample.Migrations
{
    /// <inheritdoc />
    public partial class updatetodotable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "refId",
                table: "ToDo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refId",
                table: "ToDo");
        }
    }
}

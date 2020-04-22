using Microsoft.EntityFrameworkCore.Migrations;

namespace Todo.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "TodoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 1, "To Do" });

            migrationBuilder.InsertData(
                table: "TodoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 2, "In Progress" });

            migrationBuilder.InsertData(
                table: "TodoStatus",
                columns: new[] { "Id", "Title" },
                values: new object[] { 3, "Done" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "TodoStatus",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "TodoStatus",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "TodoStatus",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace DatabaseMigration.Migrations
{
    public partial class UpdateIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "post_id",
                schema: "blogging",
                table: "posts",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "blog_id",
                schema: "blogging",
                table: "blogs",
                newName: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                schema: "blogging",
                table: "posts",
                newName: "post_id");

            migrationBuilder.RenameColumn(
                name: "id",
                schema: "blogging",
                table: "blogs",
                newName: "blog_id");
        }
    }
}

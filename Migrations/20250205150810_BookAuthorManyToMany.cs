using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookStore.Migrations
{
    /// <inheritdoc />
    public partial class BookAuthorManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AuthorModel_BookModel_BookModelID",
                table: "AuthorModel");

            migrationBuilder.DropIndex(
                name: "IX_AuthorModel_BookModelID",
                table: "AuthorModel");

            migrationBuilder.DropColumn(
                name: "BookModelID",
                table: "AuthorModel");

            migrationBuilder.AlterColumn<int>(
                name: "AgeRestriction",
                table: "BookModel",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AuthorModelBookModel",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "int", nullable: false),
                    BooksID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorModelBookModel", x => new { x.AuthorsId, x.BooksID });
                    table.ForeignKey(
                        name: "FK_AuthorModelBookModel_AuthorModel_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "AuthorModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorModelBookModel_BookModel_BooksID",
                        column: x => x.BooksID,
                        principalTable: "BookModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorModelBookModel_BooksID",
                table: "AuthorModelBookModel",
                column: "BooksID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorModelBookModel");

            migrationBuilder.AlterColumn<int>(
                name: "AgeRestriction",
                table: "BookModel",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookModelID",
                table: "AuthorModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AuthorModel_BookModelID",
                table: "AuthorModel",
                column: "BookModelID");

            migrationBuilder.AddForeignKey(
                name: "FK_AuthorModel_BookModel_BookModelID",
                table: "AuthorModel",
                column: "BookModelID",
                principalTable: "BookModel",
                principalColumn: "ID");
        }
    }
}

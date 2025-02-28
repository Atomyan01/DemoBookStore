using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoBookStore.Migrations
{
    /// <inheritdoc />
    public partial class _ : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookModel_Orders_OrderModelId",
                table: "BookModel");

            migrationBuilder.DropIndex(
                name: "IX_BookModel_OrderModelId",
                table: "BookModel");

            migrationBuilder.DropColumn(
                name: "OrderModelId",
                table: "BookModel");

            migrationBuilder.CreateTable(
                name: "BookModelOrderModel",
                columns: table => new
                {
                    BooksID = table.Column<int>(type: "int", nullable: false),
                    OrdersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookModelOrderModel", x => new { x.BooksID, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_BookModelOrderModel_BookModel_BooksID",
                        column: x => x.BooksID,
                        principalTable: "BookModel",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookModelOrderModel_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookModelOrderModel_OrdersId",
                table: "BookModelOrderModel",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookModelOrderModel");

            migrationBuilder.AddColumn<int>(
                name: "OrderModelId",
                table: "BookModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookModel_OrderModelId",
                table: "BookModel",
                column: "OrderModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookModel_Orders_OrderModelId",
                table: "BookModel",
                column: "OrderModelId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}

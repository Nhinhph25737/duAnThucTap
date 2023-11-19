using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _3_Asp.Net_MVC.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<long>(type: "bigint", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Size = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    CategoryID = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Categories",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoleID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Recipient = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bills_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_CartDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDetails_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BillDetails",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BillDetails_Bills_BillID",
                        column: x => x.BillID,
                        principalTable: "Bills",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillDetails_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Description", "Name", "Status" },
                values: new object[,]
                {
                    { new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2"), "Áo", "Áo", 1 },
                    { new Guid("d9275424-8cf1-4f51-8bc4-fb08112d99cb"), "Quần", "Quần ", 1 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "AvailableQuantity", "CategoryID", "Description", "Image", "Price", "ProductName", "Size", "Status", "TypeName" },
                values: new object[,]
                {
                    { new Guid("0b4eada7-5c76-4561-a680-f3933f3482d5"), 59, null, "Áo kaki ấm có cổ, khóa kéo", "product-4.jpg", 650000L, "Áo khoác kaki họa tiết 1", "M", 1, new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2") },
                    { new Guid("48990401-d56c-4272-9694-bd672f7484c2"), 78, null, "Áo polo mát mẻ, co dãn 4 chiều", "product-15.jpg", 350000L, "Áo polo kẻ rộng", "M", 1, new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2") },
                    { new Guid("b3c41a23-2b05-4101-81fe-36eeac9a2695"), 89, null, "Áo kaki ấm có cổ, khóa kéo", "product-2.jpg", 450000L, "Áo khoác kaki", "L", 1, new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2") },
                    { new Guid("d4648b5a-cce8-465a-a83b-4523e2b1fe86"), 59, null, "Áo phông mát mẻ, co dãn 4 chiều", "product-5.jpg", 250000L, "Áo phông họa tiết trừu tượng", "S", 1, new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2") },
                    { new Guid("d5ac8261-5791-403b-b7ba-27d9ca0d7b04"), 59, null, "Áo kaki ấm có cổ, khóa kéo", "product-12.jpg", 650000L, "Áo khoác kaki họa tiết", "M", 1, new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2") },
                    { new Guid("fa31a855-ced2-4574-a720-154fe3380f63"), 45, null, "Áo polo lịch sự", "product-8.jpg", 300000L, "Áo polo", "S", 1, new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f796e2") }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "ID", "Description", "RoleName", "Status" },
                values: new object[,]
                {
                    { new Guid("7a715fa6-026b-4fb2-9d2b-ad76cbe421b8"), "Admin", "Admin", 1 },
                    { new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f673e0"), "Khách mua hàng dùng tài khoản", "Khách hàng", 1 },
                    { new Guid("c2a00f0f-263f-4f15-b0de-3924fd81c451"), "Khách mua hàng không có tài khoản", "Khách vãng lai", 1 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Password", "RoleID", "Status", "UserName" },
                values: new object[] { new Guid("1b49d058-582a-4f3a-ae38-2f715bf75156"), null, null, new Guid("c2a00f0f-263f-4f15-b0de-3924fd81c451"), 1, null });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Password", "RoleID", "Status", "UserName" },
                values: new object[] { new Guid("5fc96e34-ea45-40b0-b655-f044e773771d"), "admin123@gmail.com", "Admin123", new Guid("7a715fa6-026b-4fb2-9d2b-ad76cbe421b8"), 1, "Admin123" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "ID", "Email", "Password", "RoleID", "Status", "UserName" },
                values: new object[] { new Guid("d76b97a6-9e9c-460e-8930-69fffea6a1e8"), "customer123@gmail.com", "Customer123", new Guid("9d47c1cb-c7d9-42c5-8654-4585f2f673e0"), 1, "Customer123" });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "ID", "Address", "CreateDate", "PhoneNumber", "Recipient", "Status", "UserID" },
                values: new object[,]
                {
                    { new Guid("05be279c-15db-435c-9805-a578222b46c5"), "Hà Nội", new DateTime(2023, 11, 19, 22, 56, 24, 320, DateTimeKind.Local).AddTicks(9126), "0879875384", "Nguyễn Văn A", 1, new Guid("1b49d058-582a-4f3a-ae38-2f715bf75156") },
                    { new Guid("0c522152-5a19-42d8-84ce-76514ce25ae8"), "Hà Nội", new DateTime(2023, 11, 19, 22, 56, 24, 320, DateTimeKind.Local).AddTicks(9105), "0943665884", "Nguyễn Văn A", 0, new Guid("d76b97a6-9e9c-460e-8930-69fffea6a1e8") },
                    { new Guid("ada30208-3268-47a7-aa35-d59486ebb6b2"), "Hà Nội", new DateTime(2023, 11, 19, 22, 56, 24, 320, DateTimeKind.Local).AddTicks(9119), "0998568984", "Nguyễn Văn A", 0, new Guid("d76b97a6-9e9c-460e-8930-69fffea6a1e8") },
                    { new Guid("bf80ab36-d28f-472a-98ab-e1b7d2d79d5e"), "Hà Nội", new DateTime(2023, 11, 19, 22, 56, 24, 320, DateTimeKind.Local).AddTicks(9123), "0345278984", "Nguyễn Văn A", 0, new Guid("1b49d058-582a-4f3a-ae38-2f715bf75156") }
                });

            migrationBuilder.InsertData(
                table: "CartDetails",
                columns: new[] { "ID", "ProductID", "Quantity", "UserID" },
                values: new object[,]
                {
                    { new Guid("1b76af66-0132-451d-8c2c-284b3ebfafc6"), new Guid("0b4eada7-5c76-4561-a680-f3933f3482d5"), 2, new Guid("d76b97a6-9e9c-460e-8930-69fffea6a1e8") },
                    { new Guid("5fc96e34-ea45-40b0-b655-f044e773771d"), new Guid("48990401-d56c-4272-9694-bd672f7484c2"), 1, new Guid("d76b97a6-9e9c-460e-8930-69fffea6a1e8") }
                });

            migrationBuilder.InsertData(
                table: "BillDetails",
                columns: new[] { "ID", "BillID", "Price", "ProductID", "Quantity" },
                values: new object[,]
                {
                    { new Guid("4efe695d-a602-4cf7-b50c-4c0da4c84524"), new Guid("ada30208-3268-47a7-aa35-d59486ebb6b2"), 650000L, new Guid("d4648b5a-cce8-465a-a83b-4523e2b1fe86"), 1 },
                    { new Guid("5ef2af34-55d5-4cdf-89d8-283c87df13f2"), new Guid("bf80ab36-d28f-472a-98ab-e1b7d2d79d5e"), 350000L, new Guid("fa31a855-ced2-4574-a720-154fe3380f63"), 2 },
                    { new Guid("64c1a8aa-2339-487f-ac07-d09423781bf6"), new Guid("0c522152-5a19-42d8-84ce-76514ce25ae8"), 300000L, new Guid("0b4eada7-5c76-4561-a680-f3933f3482d5"), 2 },
                    { new Guid("7e9d8ae1-321b-414d-a157-c638a061749c"), new Guid("bf80ab36-d28f-472a-98ab-e1b7d2d79d5e"), 650000L, new Guid("b3c41a23-2b05-4101-81fe-36eeac9a2695"), 2 },
                    { new Guid("b5e25ec7-d760-46f5-9578-d8322e437f32"), new Guid("0c522152-5a19-42d8-84ce-76514ce25ae8"), 450000L, new Guid("fa31a855-ced2-4574-a720-154fe3380f63"), 1 },
                    { new Guid("f85f7fec-df10-4f43-9fec-e9c3726707b0"), new Guid("05be279c-15db-435c-9805-a578222b46c5"), 350000L, new Guid("d4648b5a-cce8-465a-a83b-4523e2b1fe86"), 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_BillID",
                table: "BillDetails",
                column: "BillID");

            migrationBuilder.CreateIndex(
                name: "IX_BillDetails_ProductID",
                table: "BillDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_Bills_UserID",
                table: "Bills",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_ProductID",
                table: "CartDetails",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_CartDetails_UserID",
                table: "CartDetails",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryID",
                table: "Products",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BillDetails");

            migrationBuilder.DropTable(
                name: "CartDetails");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}

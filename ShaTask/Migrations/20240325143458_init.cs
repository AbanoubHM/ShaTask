using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShaTask.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: "")
                },
                constraints: table => {
                    table.PrimaryKey("PK_Cities", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BranchName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    CityID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Branches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Branches_Cities",
                        column: x => x.CityID,
                        principalTable: "Cities",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "Cashier",
                columns: table => new {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CashierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Cashier", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Cashier_Branches",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceHeader",
                columns: table => new {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    Invoicedate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    CashierID = table.Column<int>(type: "int", nullable: true),
                    BranchID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_InvoiceHeader", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceHeader_Branches",
                        column: x => x.BranchID,
                        principalTable: "Branches",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InvoiceHeader_Cashier",
                        column: x => x.CashierID,
                        principalTable: "Cashier",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "InvoiceDetails",
                columns: table => new {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceHeaderID = table.Column<long>(type: "bigint", nullable: false),
                    ItemName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false, defaultValue: ""),
                    ItemCount = table.Column<double>(type: "float", nullable: false),
                    ItemPrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_InvoiceDetails", x => x.ID);
                    table.ForeignKey(
                        name: "FK_InvoiceDetails_InvoiceHeader",
                        column: x => x.InvoiceHeaderID,
                        principalTable: "InvoiceHeader",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Branches_CityID",
                table: "Branches",
                column: "CityID");

            migrationBuilder.CreateIndex(
                name: "IX_Cashier_BranchID",
                table: "Cashier",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceDetails_InvoiceHeaderID",
                table: "InvoiceDetails",
                column: "InvoiceHeaderID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeader_BranchID",
                table: "InvoiceHeader",
                column: "BranchID");

            migrationBuilder.CreateIndex(
                name: "IX_InvoiceHeader_CashierID",
                table: "InvoiceHeader",
                column: "CashierID");
            var script = """
                SET IDENTITY_INSERT [dbo].[Cities] ON 
                
                GO
                INSERT [dbo].[Cities] ([ID], [CityName]) VALUES (1, N'القاهرة - مدينة نصر')
                GO
                INSERT [dbo].[Cities] ([ID], [CityName]) VALUES (2, N'القاهرة - القاهرة الجديدة ')
                GO
                INSERT [dbo].[Cities] ([ID], [CityName]) VALUES (3, N'القاهرة - الشروق ')
                GO
                INSERT [dbo].[Cities] ([ID], [CityName]) VALUES (4, N'القاهرة - العبور ')
                GO
                INSERT [dbo].[Cities] ([ID], [CityName]) VALUES (5, N'الاسكندرية - سموحة')
                GO
                SET IDENTITY_INSERT [dbo].[Cities] OFF
                GO
                SET IDENTITY_INSERT [dbo].[Branches] ON 

                GO
                INSERT [dbo].[Branches] ([ID], [BranchName], [CityID]) VALUES (2, N'فرع الحي السابع', 1)
                GO
                INSERT [dbo].[Branches] ([ID], [BranchName], [CityID]) VALUES (3, N'فرع عباس العقاد', 1)
                GO
                INSERT [dbo].[Branches] ([ID], [BranchName], [CityID]) VALUES (4, N'فرع التجمع الاول', 2)
                GO
                INSERT [dbo].[Branches] ([ID], [BranchName], [CityID]) VALUES (5, N'فرع سموحه', 5)
                GO
                INSERT [dbo].[Branches] ([ID], [BranchName], [CityID]) VALUES (6, N'فرع الشروق', 3)
                GO
                INSERT [dbo].[Branches] ([ID], [BranchName], [CityID]) VALUES (7, N'فرع العبور', 4)
                GO
                SET IDENTITY_INSERT [dbo].[Branches] OFF
                GO
                SET IDENTITY_INSERT [dbo].[Cashier] ON 

                GO
                INSERT [dbo].[Cashier] ([ID], [CashierName], [BranchID]) VALUES (1, N'محمد احمد', 2)
                GO
                INSERT [dbo].[Cashier] ([ID], [CashierName], [BranchID]) VALUES (2, N'محمود احمد محمد', 3)
                GO
                INSERT [dbo].[Cashier] ([ID], [CashierName], [BranchID]) VALUES (3, N'مصطفي عبد السميع', 2)
                GO
                INSERT [dbo].[Cashier] ([ID], [CashierName], [BranchID]) VALUES (4, N'احمد عبد الرحمن', 6)
                GO
                INSERT [dbo].[Cashier] ([ID], [CashierName], [BranchID]) VALUES (5, N'ساره عبد الله', 4)
                GO
                SET IDENTITY_INSERT [dbo].[Cashier] OFF
                GO
                SET IDENTITY_INSERT [dbo].[InvoiceHeader] ON 
                
                GO
                INSERT [dbo].[InvoiceHeader] ([ID], [CustomerName], [Invoicedate], [CashierID], [BranchID]) VALUES (2, N'محمد عبد الله', CAST(N'2022-02-22T00:00:00.000' AS DateTime), 1, 2)
                GO
                INSERT [dbo].[InvoiceHeader] ([ID], [CustomerName], [Invoicedate], [CashierID], [BranchID]) VALUES (3, N'محمود احمد', CAST(N'2022-02-23T00:00:00.000' AS DateTime), 2, 3)
                GO
                SET IDENTITY_INSERT [dbo].[InvoiceHeader] OFF 
                
                SET IDENTITY_INSERT [dbo].[InvoiceDetails] ON 

                GO
                INSERT [dbo].[InvoiceDetails] ([ID], [InvoiceHeaderID], [ItemName], [ItemCount], [ItemPrice]) VALUES (2, 2, N'بيبسي 1 لتر', 2, 20)
                GO
                INSERT [dbo].[InvoiceDetails] ([ID], [InvoiceHeaderID], [ItemName], [ItemCount], [ItemPrice]) VALUES (3, 2, N'ساندوتش برجر', 2, 50)
                GO
                INSERT [dbo].[InvoiceDetails] ([ID], [InvoiceHeaderID], [ItemName], [ItemCount], [ItemPrice]) VALUES (4, 2, N'ايس كريم', 1, 10)
                GO
                INSERT [dbo].[InvoiceDetails] ([ID], [InvoiceHeaderID], [ItemName], [ItemCount], [ItemPrice]) VALUES (6, 3, N'سفن اب كانز', 1, 5)
                GO
                SET IDENTITY_INSERT [dbo].[InvoiceDetails] OFF
                GO
             
                """;
            migrationBuilder.Sql(script);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InvoiceDetails");

            migrationBuilder.DropTable(
                name: "InvoiceHeader");

            migrationBuilder.DropTable(
                name: "Cashier");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}

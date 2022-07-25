using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SoftwareManagement.Infrastructure.Migrations
{
    public partial class CreateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_ProductGroup",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(150)", nullable: false),
                    Code = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_ProductGroup", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Product",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    ProductDescription = table.Column<string>(type: "nvarchar(200)", nullable: false),
                    ProductNumber = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Product", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Product_tbl_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "tbl_ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Agreement",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    ProductGroupId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductPrice = table.Column<double>(type: "float", nullable: false),
                    NewPrice = table.Column<double>(type: "float", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Agreement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tbl_Agreement_tbl_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "tbl_Product",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tbl_Agreement_tbl_ProductGroup_ProductGroupId",
                        column: x => x.ProductGroupId,
                        principalTable: "tbl_ProductGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Agreement_ProductGroupId",
                table: "tbl_Agreement",
                column: "ProductGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Agreement_ProductId",
                table: "tbl_Agreement",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Product_ProductGroupId",
                table: "tbl_Product",
                column: "ProductGroupId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Agreement");

            migrationBuilder.DropTable(
                name: "tbl_Product");

            migrationBuilder.DropTable(
                name: "tbl_ProductGroup");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab4.Migrations
{
    /// <inheritdoc />
    public partial class updateAds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Brokerage_brokerageId1",
                table: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_brokerageId1",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "brokerageId1",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "brokerageId",
                table: "Advertisements",
                newName: "BrokerageID");

            migrationBuilder.AlterColumn<string>(
                name: "BrokerageID",
                table: "Advertisements",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<int>(
                name: "ID",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_BrokerageID",
                table: "Advertisements",
                column: "BrokerageID");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Brokerage_BrokerageID",
                table: "Advertisements",
                column: "BrokerageID",
                principalTable: "Brokerage",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Advertisements_Brokerage_BrokerageID",
                table: "Advertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements");

            migrationBuilder.DropIndex(
                name: "IX_Advertisements_BrokerageID",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "BrokerageID",
                table: "Advertisements",
                newName: "brokerageId");

            migrationBuilder.AlterColumn<string>(
                name: "brokerageId",
                table: "Advertisements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "brokerageId1",
                table: "Advertisements",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Advertisements",
                table: "Advertisements",
                column: "brokerageId");

            migrationBuilder.CreateIndex(
                name: "IX_Advertisements_brokerageId1",
                table: "Advertisements",
                column: "brokerageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Advertisements_Brokerage_brokerageId1",
                table: "Advertisements",
                column: "brokerageId1",
                principalTable: "Brokerage",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

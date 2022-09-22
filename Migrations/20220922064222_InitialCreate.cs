using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraphDbApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nodes",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nodes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Edges",
                columns: table => new
                {
                    EdgeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SrcID = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false),
                    DestID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Edges", x => x.EdgeID);
                    table.ForeignKey(
                        name: "FK_Edges_Nodes_DestID",
                        column: x => x.DestID,
                        principalTable: "Nodes",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Edges_Nodes_SrcID",
                        column: x => x.SrcID,
                        principalTable: "Nodes",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Edges_DestID",
                table: "Edges",
                column: "DestID");

            migrationBuilder.CreateIndex(
                name: "IX_Edges_SrcID",
                table: "Edges",
                column: "SrcID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Edges");

            migrationBuilder.DropTable(
                name: "Nodes");
        }
    }
}

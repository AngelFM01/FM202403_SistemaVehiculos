using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialVehiculosSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "Marcas",
                schema: "dbo",
                columns: table => new
                {
                    MarcaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marcas", x => x.MarcaID);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculos",
                schema: "dbo",
                columns: table => new
                {
                    VehiculoID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MarcaID = table.Column<long>(type: "bigint", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Anio = table.Column<int>(type: "int", nullable: false),
                    CantidadPuertas = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculos", x => x.VehiculoID);
                    table.ForeignKey(
                        name: "FK_Vehiculos_Marcas_MarcaID",
                        column: x => x.MarcaID,
                        principalSchema: "dbo",
                        principalTable: "Marcas",
                        principalColumn: "MarcaID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ventas",
                schema: "dbo",
                columns: table => new
                {
                    VentaID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehiculoID = table.Column<long>(type: "bigint", nullable: false),
                    TotalVenta = table.Column<double>(type: "float", nullable: false),
                    Cantidad = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ventas", x => x.VentaID);
                    table.ForeignKey(
                        name: "FK_Ventas_Vehiculos_VehiculoID",
                        column: x => x.VehiculoID,
                        principalSchema: "dbo",
                        principalTable: "Vehiculos",
                        principalColumn: "VehiculoID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Marcas_Nombre",
                schema: "dbo",
                table: "Marcas",
                column: "Nombre",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculos_Marca",
                schema: "dbo",
                table: "Vehiculos",
                column: "MarcaID");

            migrationBuilder.CreateIndex(
                name: "IX_Ventas_Vehiculo",
                schema: "dbo",
                table: "Ventas",
                column: "VehiculoID",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ventas",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Vehiculos",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "Marcas",
                schema: "dbo");
        }
    }
}

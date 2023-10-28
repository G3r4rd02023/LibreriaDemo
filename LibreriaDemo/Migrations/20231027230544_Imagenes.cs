using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibreriaDemo.Migrations
{
    /// <inheritdoc />
    public partial class Imagenes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "URLImagen",
                table: "Libros",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "URLImagen",
                table: "Libros");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ComprasVentas.Migrations
{
    /// <inheritdoc />
    public partial class TestChangePermisoRol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario_rol",
                table: "usuario_rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permiso_rol",
                table: "permiso_rol");

            migrationBuilder.CreateIndex(
                name: "IX_usuario_rol_RolesId",
                table: "usuario_rol",
                column: "RolesId");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_rol_PermisosId",
                table: "permiso_rol",
                column: "PermisosId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuario_rol_RolesId",
                table: "usuario_rol");

            migrationBuilder.DropIndex(
                name: "IX_permiso_rol_PermisosId",
                table: "permiso_rol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario_rol",
                table: "usuario_rol",
                columns: new[] { "RolesId", "UsuariosId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_permiso_rol",
                table: "permiso_rol",
                columns: new[] { "PermisosId", "RolesId" });
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComprasVentas.Migrations
{
    /// <inheritdoc />
    public partial class TestFinalMigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Personas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 2, 4 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 4, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 5, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 6, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 6, 3 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 6, 4 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 7, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 7, 3 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 8, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 9, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 9, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 10, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 10, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 10, 4 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 11, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 11, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 11, 3 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 12, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 12, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 13, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 14, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 15, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 16, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 17, 1 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 17, 2 });

            migrationBuilder.DeleteData(
                table: "permiso_rol",
                keyColumns: new[] { "PermisosId", "RolesId" },
                keyValues: new object[] { 17, 3 });

            migrationBuilder.DeleteData(
                table: "usuario_rol",
                keyColumns: new[] { "RolesId", "UsuariosId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "usuario_rol",
                keyColumns: new[] { "RolesId", "UsuariosId" },
                keyValues: new object[] { 2, 2 });

            migrationBuilder.DeleteData(
                table: "usuario_rol",
                keyColumns: new[] { "RolesId", "UsuariosId" },
                keyValues: new object[] { 3, 3 });

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Permisos",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: 3);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Permisos",
                columns: new[] { "Id", "Accion", "Nombre", "Recurso" },
                values: new object[,]
                {
                    { 1, "CREATE", "ROL_CREATE", "ROL" },
                    { 2, "READ", "ROL_READ", "ROL" },
                    { 3, "UPDATE", "ROL_UPDATE", "ROL" },
                    { 4, "DELETE", "ROL_DELETE", "ROL" },
                    { 5, "CREATE", "USUARIO_CREATE", "USUARIO" },
                    { 6, "READ", "USUARIO_READ", "USUARIO" },
                    { 7, "UPDATE", "USUARIO_UPDATE", "USUARIO" },
                    { 8, "DELETE", "USUARIO_DELETE", "USUARIO" },
                    { 9, "CREATE", "PERSONA_CREATE", "PERSONA" },
                    { 10, "READ", "PERSONA_READ", "PERSONA" },
                    { 11, "UPDATE", "PERSONA_UPDATE", "PERSONA" },
                    { 12, "DELETE", "PERSONA_DELETE", "PERSONA" },
                    { 13, "CREATE", "PERMISO_CREATE", "PERMISO" },
                    { 14, "READ", "PERMISO_READ", "PERMISO" },
                    { 15, "UPDATE", "PERMISO_UPDATE", "PERMISO" },
                    { 16, "DELETE", "PERMISO_DELETE", "PERMISO" },
                    { 17, "READ", "DASHBOARD_READ", "DASHBOARD" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Descripcion", "Nombre" },
                values: new object[,]
                {
                    { 1, "Acceso completo a todos los recursos del sistema", "Super Administrador" },
                    { 2, "Administración del sistema (sin permisos de super administrador)", "Administrador" },
                    { 3, "Usuario regular del sistema con permisos limitados", "Usuario" },
                    { 4, "Usuario con acceso de solo lectura", "Invitado" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Correo", "Nombre", "Password" },
                values: new object[,]
                {
                    { 1, "superadmin@comprasventas.com", "superadmin", "$2a$11$JvrSYkK8p.kpqsW5BoyIweIdDgJJGm6r0nL9.Qd/fYHB85pLuBGhy" },
                    { 2, "admin@comprasventas.com", "admin", "$2a$11$GfnhbgoQRmPxVhOAhigSQOeXyoccrmPDTKnA3N1RszFvQQpbi4niK" },
                    { 3, "usuario1@comprasventas.com", "usuario1", "$2a$11$cWGKiuqlX3PriTe15fYODOMHE/C26X8GxPxx4gylTnkl6uHfYtj/e" }
                });

            migrationBuilder.InsertData(
                table: "Personas",
                columns: new[] { "Id", "Apellidos", "Direccion", "FechaNacimiento", "Genero", "Nacionalidad", "Nombres", "Telefono" },
                values: new object[,]
                {
                    { 1, "Pérez", "Calle Principal 123", new DateTime(1985, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Masculino", "Mexicana", "Juan", "+1234567890" },
                    { 2, "González", "Avenida Central 456", new DateTime(1990, 8, 22, 0, 0, 0, 0, DateTimeKind.Utc), "Femenino", "Mexicana", "María", "+9876543210" },
                    { 3, "López", "Plaza Mayor 789", new DateTime(1995, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Masculino", "Mexicana", "Carlos", "+1122334455" }
                });

            migrationBuilder.InsertData(
                table: "permiso_rol",
                columns: new[] { "PermisosId", "RolesId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 4 },
                    { 3, 1 },
                    { 3, 2 },
                    { 4, 1 },
                    { 4, 2 },
                    { 5, 1 },
                    { 5, 2 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 4 },
                    { 7, 1 },
                    { 7, 2 },
                    { 7, 3 },
                    { 8, 1 },
                    { 8, 2 },
                    { 9, 1 },
                    { 9, 2 },
                    { 10, 1 },
                    { 10, 2 },
                    { 10, 3 },
                    { 10, 4 },
                    { 11, 1 },
                    { 11, 2 },
                    { 11, 3 },
                    { 12, 1 },
                    { 12, 2 },
                    { 13, 1 },
                    { 14, 1 },
                    { 15, 1 },
                    { 16, 1 },
                    { 17, 1 },
                    { 17, 2 },
                    { 17, 3 }
                });

            migrationBuilder.InsertData(
                table: "usuario_rol",
                columns: new[] { "RolesId", "UsuariosId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 }
                });
        }
    }
}

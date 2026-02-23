using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ComprasVentas.Migrations
{
    /// <inheritdoc />
    public partial class CompletedApp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_usuario_rol_UsuariosId",
                table: "usuario_rol");

            migrationBuilder.DropIndex(
                name: "IX_permiso_rol_PermisosId",
                table: "permiso_rol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_usuario_rol",
                table: "usuario_rol",
                columns: new[] { "UsuariosId", "RolesId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_permiso_rol",
                table: "permiso_rol",
                columns: new[] { "PermisosId", "RolesId" });

            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteProveedor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Tipo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    RazonSocial = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    NroIdentificacion = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Direccion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Correo = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteProveedor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sucursales",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ciudad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sucursales", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Productos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    UnidadMedida = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Marca = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    PrecioVentaActual = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    Imagen = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    CategoriaId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Productos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Notas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TipoNota = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Impuestos = table.Column<decimal>(type: "numeric(13,2)", precision: 13, scale: 2, nullable: false),
                    Total = table.Column<decimal>(type: "numeric(13,2)", precision: 13, scale: 2, nullable: false),
                    Descuentos = table.Column<decimal>(type: "numeric(13,2)", precision: 13, scale: 2, nullable: false),
                    Estado = table.Column<bool>(type: "boolean", nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false),
                    ClienteProveedorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notas_ClienteProveedor_ClienteProveedorId",
                        column: x => x.ClienteProveedorId,
                        principalTable: "ClienteProveedor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Notas_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Almacenes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Codigo = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Descripcion = table.Column<string>(type: "text", nullable: true),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Ciudad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    SucursalId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Almacenes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Almacenes_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SucursalUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaAsignacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    SucursalId = table.Column<int>(type: "integer", nullable: true),
                    UsuarioId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SucursalUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SucursalUsers_Sucursales_SucursalId",
                        column: x => x.SucursalId,
                        principalTable: "Sucursales",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_SucursalUsers_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlmacenProductos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CantidadActual = table.Column<int>(type: "integer", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ProductoId = table.Column<int>(type: "integer", nullable: true),
                    AlmacenId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlmacenProductos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlmacenProductos_Almacenes_AlmacenId",
                        column: x => x.AlmacenId,
                        principalTable: "Almacenes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AlmacenProductos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Movimientos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Cantidad = table.Column<decimal>(type: "numeric", nullable: false),
                    TipoMovimiento = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    PrecioUnitarioCompra = table.Column<decimal>(type: "numeric(13,2)", precision: 13, scale: 2, nullable: false),
                    PrecioUnitarioVenta = table.Column<decimal>(type: "numeric(13,2)", precision: 13, scale: 2, nullable: false),
                    Observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ProductoId = table.Column<int>(type: "integer", nullable: false),
                    AlmacenId = table.Column<int>(type: "integer", nullable: false),
                    NotaId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimientos_Almacenes_AlmacenId",
                        column: x => x.AlmacenId,
                        principalTable: "Almacenes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientos_Notas_NotaId",
                        column: x => x.NotaId,
                        principalTable: "Notas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Movimientos_Productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "Productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Almacenes_SucursalId",
                table: "Almacenes",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_AlmacenProductos_AlmacenId",
                table: "AlmacenProductos",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_AlmacenProductos_ProductoId",
                table: "AlmacenProductos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_AlmacenId",
                table: "Movimientos",
                column: "AlmacenId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_NotaId",
                table: "Movimientos",
                column: "NotaId");

            migrationBuilder.CreateIndex(
                name: "IX_Movimientos_ProductoId",
                table: "Movimientos",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_ClienteProveedorId",
                table: "Notas",
                column: "ClienteProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_Notas_UsuarioId",
                table: "Notas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Productos_CategoriaId",
                table: "Productos",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalUsers_SucursalId",
                table: "SucursalUsers",
                column: "SucursalId");

            migrationBuilder.CreateIndex(
                name: "IX_SucursalUsers_UsuarioId",
                table: "SucursalUsers",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlmacenProductos");

            migrationBuilder.DropTable(
                name: "Movimientos");

            migrationBuilder.DropTable(
                name: "SucursalUsers");

            migrationBuilder.DropTable(
                name: "Almacenes");

            migrationBuilder.DropTable(
                name: "Notas");

            migrationBuilder.DropTable(
                name: "Productos");

            migrationBuilder.DropTable(
                name: "Sucursales");

            migrationBuilder.DropTable(
                name: "ClienteProveedor");

            migrationBuilder.DropTable(
                name: "Categorias");

            migrationBuilder.DropPrimaryKey(
                name: "PK_usuario_rol",
                table: "usuario_rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_permiso_rol",
                table: "permiso_rol");

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

            migrationBuilder.CreateIndex(
                name: "IX_usuario_rol_UsuariosId",
                table: "usuario_rol",
                column: "UsuariosId");

            migrationBuilder.CreateIndex(
                name: "IX_permiso_rol_PermisosId",
                table: "permiso_rol",
                column: "PermisosId");
        }
    }
}

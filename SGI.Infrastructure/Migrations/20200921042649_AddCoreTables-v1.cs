using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SGI.Infrastructure.Migrations
{
    public partial class AddCoreTablesv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dealers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "varchar(200)", nullable: false),
                    LocationCode = table.Column<string>(type: "varchar(200)", nullable: true),
                    DistributorCode = table.Column<string>(type: "varchar(200)", nullable: true),
                    Country = table.Column<string>(type: "varchar(200)", nullable: true),
                    State = table.Column<string>(type: "varchar(200)", nullable: true),
                    City = table.Column<string>(type: "varchar(200)", nullable: true),
                    Address = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(200)", nullable: true),
                    Fax = table.Column<string>(type: "varchar(200)", nullable: true),
                    Zip = table.Column<string>(type: "varchar(200)", nullable: true),
                    Email = table.Column<string>(type: "varchar(200)", nullable: true),
                    Website = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    LatitudGps = table.Column<float>(type: "float", nullable: true),
                    LongitudGps = table.Column<float>(type: "float", nullable: true),
                    SkipLongitude = table.Column<string>(type: "varchar(MAX)", nullable: true)

                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dealers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumeroIncidencia = table.Column<long>(type: "bigint", nullable: false),
                    NumeroOperacion = table.Column<long>(type: "bigint", nullable: false),
                    ConfiguracionCorta = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    FechaPreEntrega = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaIncidencia = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaRegistro = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaCierre = table.Column<DateTime>(type: "datetime", nullable: true),
                    NroReclamoConcesionario = table.Column<int>(type: "int", nullable: true),
                    NroReclamoCummins = table.Column<int>(type: "int", nullable: true),
                    Equipo = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ModeloEquipo = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DireccionInspeccion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    LatitudGps = table.Column<float>(type: "float", nullable: true),
                    LongitudGps = table.Column<float>(type: "float", nullable: true),
                    PathImagenes = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    MostrarEnTv = table.Column<int>(type: "int", nullable: true),
                    Aux1 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Aux2 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Aux3 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Sintoma = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ImagenComprobante = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    EsGarantia = table.Column<bool>(type: "bit", nullable: true),
                    Aux4 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Aux5 = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IdDealer = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Incidencias_Dealers_IdDealer",
                        column: x => x.IdDealer,
                        principalTable: "Dealers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Incidencias_IdDealer",
                table: "Incidencias",
                column: "IdDealer");

            migrationBuilder.CreateTable(
                name: "Estados",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Codigo = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estados", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Nombre = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Telefono = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Email = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Direccion = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    DNI = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    TipoDNI = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Celular = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Localidad = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Provincia = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    LatitudGpsContacto = table.Column<float>(type: "float", nullable: true),
                    LongitudGpsContacto = table.Column<float>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });


            migrationBuilder.CreateTable(
                name: "EstadoGarantia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    Codigo = table.Column<int>(type: "int", nullable: false).Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "varchar(200)", nullable: true),
                    ObservacionesGarantia = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ObservacionesProveedor = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IdIncidencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoGarantia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadoGarantia_Incidencias_IdIncidencia",
                        column: x => x.IdIncidencia,
                        principalTable: "Incidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EstadoGarantia_IdIncidencia",
                table: "EstadoGarantia",
                column: "IdIncidencia");

            migrationBuilder.CreateTable(
                name: "Falla",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    IdFalla = table.Column<int>(type: "int", nullable: true),
                    Codigo = table.Column<string>(type: "varchar(100)", nullable: true),
                    Observaciones = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    IdIncidencia = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Falla", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Falla_Incidencias_IdIncidencia",
                        column: x => x.IdIncidencia,
                        principalTable: "Incidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Falla_IdIncidencia",
                table: "Falla",
                column: "IdIncidencia");

            migrationBuilder.CreateTable(
                name: "Motor",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    NumeroMotor = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    NumeroChasis = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    Modelo = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    HsKm = table.Column<int>(type: "int", nullable: true),
                    Equipo = table.Column<string>(type: "varchar(MAX)", nullable: true),
                    ModeloEquipo = table.Column<string>(type: "varchar(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Motor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClienteIncidencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    ClienteId = table.Column<Guid>(nullable: false),
                    IncidenciaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClienteIncidencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClienteIncidencia_Incidencias_IncidenciaId",
                        column: x => x.IncidenciaId,
                        principalTable: "Incidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClienteIncidencia_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MotorIncidencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaCompra = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaInicioGarantia = table.Column<DateTime>(type: "datetime", nullable: true),
                    FechaFalla = table.Column<DateTime>(type: "datetime", nullable: true),
                    MotorId = table.Column<Guid>(nullable: false),
                    IncidenciaId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MotorIncidencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MotorIncidencia_Incidencias_IncidenciaId",
                        column: x => x.IncidenciaId,
                        principalTable: "Incidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MotorIncidencia_Motor_MotorId",
                        column: x => x.MotorId,
                        principalTable: "Motor",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EstadoIncidencia",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "getdate()"),
                    Modified = table.Column<DateTime>(type: "datetime", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime", nullable: true),
                    IncidenciaId = table.Column<Guid>(nullable: false),
                    EstadoId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EstadoIncidencia", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EstadoIncidencia_Incidencias_IncidenciaId",
                        column: x => x.IncidenciaId,
                        principalTable: "Incidencias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EstadoIncidencia_Estados_EstadoId",
                        column: x => x.EstadoId,
                        principalTable: "Estados",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MotorIncidencia_MotorId",
                table: "MotorIncidencia",
                column: "MotorId");

            migrationBuilder.CreateIndex(
                name: "IX_MotorIncidencia_IncidenciaId",
                table: "MotorIncidencia",
                column: "IncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoIncidencia_EstadoId",
                table: "EstadoIncidencia",
                column: "EstadoId");

            migrationBuilder.CreateIndex(
                name: "IX_EstadoIncidencia_IncidenciaId",
                table: "EstadoIncidencia",
                column: "IncidenciaId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteIncidencia_ClienteId",
                table: "ClienteIncidencia",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_ClienteIncidencia_IncidenciaId",
                table: "ClienteIncidencia",
                column: "IncidenciaId");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MotorIncidencia");

            migrationBuilder.DropTable(
                name: "EstadoIncidencia");

            migrationBuilder.DropTable(
                name: "Motor");

            migrationBuilder.DropTable(
                name: "Estados");

            migrationBuilder.DropTable(
                name: "EstadosGarantia");

            migrationBuilder.DropTable(
                name: "Falla");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "Dealers");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class participante_to_convidado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Participantes_Churrascos_ChurrascoId",
                table: "Participantes");

            migrationBuilder.DropForeignKey(
                name: "FK_Participantes_Opcoes_OpcaoId",
                table: "Participantes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Participantes",
                table: "Participantes");

            migrationBuilder.RenameTable(
                name: "Participantes",
                newName: "Convidados");

            migrationBuilder.RenameIndex(
                name: "IX_Participantes_OpcaoId",
                table: "Convidados",
                newName: "IX_Convidados_OpcaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Participantes_ChurrascoId",
                table: "Convidados",
                newName: "IX_Convidados_ChurrascoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Convidados",
                table: "Convidados",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Convidados_Churrascos_ChurrascoId",
                table: "Convidados",
                column: "ChurrascoId",
                principalTable: "Churrascos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Convidados_Opcoes_OpcaoId",
                table: "Convidados",
                column: "OpcaoId",
                principalTable: "Opcoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Convidados_Churrascos_ChurrascoId",
                table: "Convidados");

            migrationBuilder.DropForeignKey(
                name: "FK_Convidados_Opcoes_OpcaoId",
                table: "Convidados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Convidados",
                table: "Convidados");

            migrationBuilder.RenameTable(
                name: "Convidados",
                newName: "Participantes");

            migrationBuilder.RenameIndex(
                name: "IX_Convidados_OpcaoId",
                table: "Participantes",
                newName: "IX_Participantes_OpcaoId");

            migrationBuilder.RenameIndex(
                name: "IX_Convidados_ChurrascoId",
                table: "Participantes",
                newName: "IX_Participantes_ChurrascoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Participantes",
                table: "Participantes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Participantes_Churrascos_ChurrascoId",
                table: "Participantes",
                column: "ChurrascoId",
                principalTable: "Churrascos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Participantes_Opcoes_OpcaoId",
                table: "Participantes",
                column: "OpcaoId",
                principalTable: "Opcoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

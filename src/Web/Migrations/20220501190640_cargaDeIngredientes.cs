using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    public partial class cargaDeIngredientes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"insert into ingredientes (descripcion) values(	'Acedera');
            insert into ingredientes(descripcion) values('Acelga');
            insert into ingredientes(descripcion) values('Achicoria (escarola o achicoria común)');
            insert into ingredientes(descripcion) values('Aguacate/Palta');
            insert into ingredientes(descripcion) values('Ajo');
            insert into ingredientes(descripcion) values('Albahaca');
            insert into ingredientes(descripcion) values('Alcachofa');
            insert into ingredientes(descripcion) values('Alcaravea (o alcarabia, alcarahueya, carvia, caravai, alcaravia, comino de prado o alcaraveta)');
            insert into ingredientes(descripcion) values('Alubias (blancas, rojas)');
            insert into ingredientes(descripcion) values('Anís');
            insert into ingredientes(descripcion) values('Apio');
            insert into ingredientes(descripcion) values('Apio de monte (levístico)');
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from Ingredientes");
        }
    }
}

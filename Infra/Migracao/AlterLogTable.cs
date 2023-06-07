using FluentMigrator;

namespace Infra.Migracao
{
    [Migration(20230607)]
    public class AlterLogTable : Migration
    {
        public override void Up()
        {
            const int tamanhoDaColuna = 18888;
            Alter.Table("CadastroCliente")
                .AddColumn("Imagem_Usuario").AsString(tamanhoDaColuna).Nullable();
        }

        public override void Down()
        {
            Delete.Column("Imagem_Usuario");
        }
    }
}

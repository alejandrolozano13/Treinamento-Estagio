using FluentMigrator;

namespace TreinamentoInvent
{
    [Migration(20230426)]
    public class AddLogTable : Migration
    {
        public override void Up()
        {
            Create.Table("Cadastro")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString()
                .WithColumn("CPF").AsString()
                .WithColumn("Telefone").AsString()
                .WithColumn("E-Mail").AsString()
                .WithColumn("Data de Nascimento").AsDate();
        }

        public override void Down()
        {
            Delete.Table("Cdastro");
        }
    }
}

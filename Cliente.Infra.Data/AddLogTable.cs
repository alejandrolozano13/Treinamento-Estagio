using FluentMigrator;

namespace TreinamentoInvent
{
    [Migration(20230426)]
    public class AddLogTable : Migration
    {
        public override void Up()
        {
            Create.Table("CadastroCliente")
                .WithColumn("Id").AsInt64().PrimaryKey().Identity()
                .WithColumn("Nome").AsString().NotNullable()
                .WithColumn("CPF").AsString().NotNullable()
                .WithColumn("Telefone").AsString().NotNullable()
                .WithColumn("EMail").AsString().NotNullable()
                .WithColumn("Data_Nascimento").AsDate().NotNullable();
        }

        public override void Down()
        {
            Delete.Table("CadastroCliente");
        }
    }
}

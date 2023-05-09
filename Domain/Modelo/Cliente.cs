using LinqToDB.Mapping;
using System;

namespace Domain.Modelo
{
    [Table("CadastroCliente")]
    public class Cliente
    {
        [PrimaryKey, Identity]
        public int Id { get; set; }

        [Column("CPF"), NotNull]
        public string Cpf { get; set; }

        [Column("Nome"), NotNull]
        public string Nome { get; set; }

        [Column("EMail"), NotNull]
        public string Email { get; set; }

        [Column("Telefone"), NotNull]
        public string Telefone { get; set; }

        [Column("Data_Nascimento"), NotNull]
        public DateTime Data { get; set; }
    }
}

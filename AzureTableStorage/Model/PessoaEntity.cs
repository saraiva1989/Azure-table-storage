using System;
using Microsoft.Azure.Cosmos.Table;

namespace AzureTableStorage.Model
{
    //precisa extender a classe TableEntity do azure, e precisa dos construtores
    //Vazio e um referente ao partitionkey e rowkey
    public class PessoaEntity : TableEntity
    {
        public PessoaEntity() { }

        public PessoaEntity(PessoaEntity pessoa)
        {
            this.PartitionKey = pessoa.Email;
            this.RowKey = pessoa.Nome;
        }

        public string Email { get; set; }
        public string Nome { get; set; }
        public string Endereco { get; set; }
    }
}

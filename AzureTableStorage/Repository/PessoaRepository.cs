using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTableStorage.Interface;
using AzureTableStorage.Model;
using Microsoft.Azure.Cosmos.Table;

namespace AzureTableStorage.Service
{
    public class PessoaRepository : IPessoaRepository
    {
        static CloudStorageAccount _storageAccount;
        static CloudTable cloudTable;

        public PessoaRepository (string storageAccount)
        {
            _storageAccount = CloudStorageAccount.Parse(storageAccount);
            CloudTableClient tableClient = _storageAccount.CreateCloudTableClient();
            cloudTable = tableClient.GetTableReference("Pessoa");
            cloudTable.CreateIfNotExists();
        }


        public bool InserirPessoa(PessoaEntity pessoa)
        {
            PessoaEntity pessoaEntity = new PessoaEntity(pessoa)
            {
                Email = pessoa.Email,
                Nome = pessoa.Nome,
                Endereco = pessoa.Endereco
            };

            TableOperation operecaoInserir = TableOperation.Insert(pessoaEntity);
            try
            {
                cloudTable.Execute(operecaoInserir);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Atualizar(PessoaEntity pessoa)
        {
            PessoaEntity pessoaEntity = new PessoaEntity(pessoa)
            {
                Email = pessoa.Email,
                Nome = pessoa.Nome,
                Endereco = pessoa.Endereco,
                //nessessário passar o * se quiser forçar a atualização
                ETag = "*"
            };

            TableOperation operecaoAtualizar = TableOperation.Merge(pessoaEntity);
            try
            {
                cloudTable.Execute(operecaoAtualizar);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Deletar(PessoaEntity pessoa)
        {
            PessoaEntity pessoaEntity = new PessoaEntity(pessoa)
            {
                Email = pessoa.Email,
                Nome = pessoa.Nome,
                Endereco = pessoa.Endereco,
                ETag = "*"
            };

            TableOperation operacaoDeletar = TableOperation.Delete(pessoaEntity);
            try
            {
                cloudTable.Execute(operacaoDeletar);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<PessoaEntity> ListarTodasPessoas()
        {
            TableQuery<PessoaEntity> getQuery = new TableQuery<PessoaEntity>();
            var listaPessoa = cloudTable.ExecuteQuery(getQuery);
            return listaPessoa;
        }

        public PessoaEntity ListarPelaChave(PessoaEntity pessoa)
        {
            PessoaEntity pessoaEntity = new PessoaEntity(pessoa);
            TableOperation buscar = TableOperation.Retrieve<PessoaEntity>(pessoaEntity.PartitionKey, pessoaEntity.RowKey);
            TableResult result = cloudTable.Execute(buscar);
            PessoaEntity Pessoa = result.Result as PessoaEntity;
            return Pessoa;
        }
    }
}

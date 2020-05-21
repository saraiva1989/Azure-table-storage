using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureTableStorage.Model;

namespace AzureTableStorage.Interface
{
    public interface IPessoaRepository
    {
        IEnumerable<PessoaEntity> ListarTodasPessoas();
        PessoaEntity ListarPelaChave(PessoaEntity pessoa);
        bool InserirPessoa(PessoaEntity pessoa);
        bool Atualizar(PessoaEntity pessoa);
        bool Deletar(PessoaEntity pessoa);
    }
}

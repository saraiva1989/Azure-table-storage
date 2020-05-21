using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureTableStorage.Interface;
using AzureTableStorage.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureTableStorage.Controllers
{
    [Route("api/[controller]")]
    public class PessoaController : Controller
    {
        private readonly IPessoaRepository _pessoa;

        public PessoaController (IPessoaRepository pessoa)
        {
            _pessoa = pessoa;
        }

        [HttpGet]
        public IEnumerable<PessoaEntity> Get()
        {
            return _pessoa.ListarTodasPessoas();
        }

        [HttpPost]
        [Route ("ListarPorChave")]
        public PessoaEntity ListarPorChave([FromBody] PessoaEntity pessoaEntity)
        {
            return _pessoa.ListarPelaChave(pessoaEntity);
        }

        [HttpPost]
        public Dictionary<string, string> Post([FromBody] PessoaEntity pessoaEntity)
        {
            bool retorno = _pessoa.InserirPessoa(pessoaEntity);
            string mensagem = !retorno ? "erro ao salvar o registro" : "registro salvo com sucesso";
            return new Dictionary<string, string>
            {
                { "Mensagem", mensagem }
            };
        }

        [HttpPut]
        public Dictionary<string, string> Put([FromBody] PessoaEntity pessoaEntity)
        {
            bool retorno = _pessoa.Atualizar(pessoaEntity);
            string mensagem = !retorno ? "registro não encontrado" : "registro atualizado com sucesso";
            return new Dictionary<string, string>
            {
                { "Mensagem", mensagem }
            };
        }

        [HttpDelete]
        public Dictionary<string, string> Delete([FromBody] PessoaEntity pessoaEntity)
        {
            bool retorno = _pessoa.Deletar(pessoaEntity);
            string mensagem = !retorno ? "registro não encontrado" : "registro removido com sucesso";
            return new Dictionary<string, string>
            {
                { "Mensagem", mensagem }
            };
        }
    }
}

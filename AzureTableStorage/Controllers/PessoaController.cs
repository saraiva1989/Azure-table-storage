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

        // GET: api/values
        [HttpGet]
        public IEnumerable<PessoaEntity> Get()
        {
            return _pessoa.ListarTodasPessoas();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        [Route ("ListarPorChave")]
        public PessoaEntity ListarPorChave([FromBody] PessoaEntity pessoaEntity)
        {
            return _pessoa.ListarPelaChave(pessoaEntity);
        }

        // POST api/values
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

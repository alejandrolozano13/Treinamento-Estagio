using Domain.BancoDeDados;
using Domain.Modelo;
using Domain.Validacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Net;

namespace SistemaCadastroWeb.Controllers
{
    [Route("api/Cadastro")]
    [ApiController]
    public class CadastroController : ControllerBase
    {
        private readonly IRepositorio _repostorio;
        private readonly Validacoes _validacoes;

        public CadastroController(IRepositorio repositorio, Validacoes validacoes)
        {
            _repostorio = repositorio;
            _validacoes = validacoes;
        }

        [HttpGet]
        public IActionResult OberTodos()
        {
            try
            {
                BindingList<Cliente> clientes = _repostorio.ObterTodos();
                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                var cliente = _repostorio.ObterPorId(id);

                return Ok(cliente);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + "," + ex.InnerException);
            }
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Cliente cliente)
        {
            if (cliente == null) { return BadRequest(); }
            try
            {
                _validacoes.ValidarCliente(cliente, false);
                _repostorio.Criar(cliente);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] Cliente cliente)
        {
            try
            {
                _validacoes.ValidarCliente(cliente, true);
                _repostorio.Atualizar(cliente);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remover(int id)
        {
            try
            {
                _repostorio.Remover(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

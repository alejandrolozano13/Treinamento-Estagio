using Domain.BancoDeDados;
using Domain.Modelo;
using Domain.Validacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SistemaCadastroWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IRepositorio _repostorio;
        private readonly ValidadorDeCliente _validadorDeCliente;

        public ClienteController(IRepositorio repositorio, ValidadorDeCliente validacoes)
        {
            _repostorio = repositorio;
            _validadorDeCliente = validacoes;
        }

        [HttpGet]
        public IActionResult OberTodos()
        {
            try
            {
                List<Cliente> clientes = _repostorio.ObterTodos().ToList();
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
                _validadorDeCliente.Validar(cliente, false);
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
                _validadorDeCliente.Validar(cliente, true);
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

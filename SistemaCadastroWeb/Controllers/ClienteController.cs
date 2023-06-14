using Domain.BancoDeDados;
using Domain.Modelo;
using Domain.Validacao;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

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
        public IActionResult OberTodos([FromQuery] string? nome)
        {
            try
            {
                var clientes = _repostorio.ObterTodos(nome);

                return Ok(clientes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
        

        [HttpGet("{id}")]
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
                var novoCliente = _repostorio.pesquisarPeloCpf(cliente.Cpf);

                return Ok(novoCliente.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody, Required()] Cliente cliente, int id)
        {
            try
            {
                cliente.Id = id;
                _repostorio.Atualizar(cliente);
                _validadorDeCliente.Validar(cliente, true);
                
                return Ok(cliente);
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

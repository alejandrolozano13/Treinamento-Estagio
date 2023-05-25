﻿using Domain.BancoDeDados;
using Domain.Modelo;
using Domain.Validacao;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace SistemaCadastroWeb.Controllers
{
    [Route("api/[controller]")]
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
                return BadRequest();
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            try
            {
                Cliente cliente = _repostorio.ObterPorId(id);
                return Ok(cliente);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
            
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Cliente cliente)
        {
            try
            {
                _validacoes.ValidarCliente(cliente, false);
                _repostorio.Criar(cliente);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromBody] Cliente cliente, int id)
        {
            try
            {
                cliente.Id = id;
                _validacoes.ValidarCliente(cliente, true);
                _repostorio.Atualizar(id, cliente);
                return Ok();
            } catch (Exception ex)
            {
                return BadRequest();
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
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Franquias.Api.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace Franquias.Api.Controllers
{
   [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/unidades")]
    public class UnidadeFranqueadaController : ControllerBase
    {
        private readonly UnidadeFranqueadaService _service;
        private readonly FranqueadoRepository _franqueadoRepositorio;

        public UnidadeFranqueadaController(UnidadeFranqueadaService service, FranqueadoRepository franqueadoRepositorio)
        {
            _service = service;
            _franqueadoRepositorio = franqueadoRepositorio;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            string? nome, string? cidade, string? cnpj, string? situacao, string? responsavel,
            string ordenarPor = "nome", bool decrescente = false,
            int pagina = 1, int tamanhoPagina = 10)
        {
            var unidades = await _service.ListarTodosAsync();

            if (!string.IsNullOrEmpty(nome))
            {
                unidades = unidades.Where(u => u.Nome.Contains(nome)).ToList();
            }
            if (!string.IsNullOrEmpty(cidade))
            {
                unidades = unidades.Where(u => u.Cidade.Contains(cidade)).ToList();
            }
            if (!string.IsNullOrEmpty(cnpj))
            {
                unidades = unidades.Where(u => u.CNPJ.Contains(cnpj)).ToList();
            }
            if (!string.IsNullOrEmpty(situacao))
            {
                unidades = unidades.Where(u => u.Situacao == situacao).ToList();
            }
            if (!string.IsNullOrEmpty(responsavel))
            {
                var franqueados = await _franqueadoRepositorio.ListarTodosAsync();
                var unidadeIds = franqueados
                    .Where(f => f.Nome.Contains(responsavel, StringComparison.OrdinalIgnoreCase))
                    .Select(f => f.UnidadeFranqueadaId)
                    .ToHashSet();

                unidades = unidades.Where(u => unidadeIds.Contains(u.Id)).ToList();
            }

            unidades = ordenarPor.ToLower() switch
            {
                "cidade" => decrescente ? unidades.OrderByDescending(u => u.Cidade).ToList() : unidades.OrderBy(u => u.Cidade).ToList(),
                _ => decrescente ? unidades.OrderByDescending(u => u.Nome).ToList() : unidades.OrderBy(u => u.Nome).ToList()
            };

            var total = unidades.Count;
            var itens = unidades.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

            return Ok(new { Total = total, Pagina = pagina, TamanhoPagina = tamanhoPagina, Itens = itens });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var unidade = await _service.BuscarPorIdAsync(id);
            if (unidade == null)
            {
                return NotFound("Unidade não encontrada.");
            }
            return Ok(unidade);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UnidadeFranqueadaDTO dto)
        {
            try
            {
                var unidade = new UnidadeFranqueada
                {
                    Nome = dto.Nome,
                    CNPJ = dto.CNPJ,
                    Endereco = dto.Endereco,
                    Cidade = dto.Cidade,
                    Telefone = dto.Telefone,
                    DataInicio = dto.DataInicio,
                    FranqueadoraId = dto.FranqueadoraId,
                    Situacao = "Ativa"
                };

                await _service.AdicionarAsync(unidade);
                var unidadeCompleta = await _service.BuscarPorIdAsync(unidade.Id);

                return Ok(unidadeCompleta);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, UnidadeFranqueadaDTO dto)
        {
            var unidade = await _service.BuscarPorIdAsync(id);
            if (unidade == null)
            {
                return NotFound("Unidade não encontrada.");
            }

            unidade.Nome = dto.Nome;
            unidade.Endereco = dto.Endereco;
            unidade.Cidade = dto.Cidade;
            unidade.Telefone = dto.Telefone;

            await _service.AtualizarAsync(unidade);
            return Ok(unidade);
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> Inativar(int id)
        {
            try
            {
                await _service.InativarAsync(id);
                return Ok("Unidade inativada com sucesso.");
            }
            catch (Exception erro)
            {
                return NotFound(erro.Message);
            }
        }
    }
}
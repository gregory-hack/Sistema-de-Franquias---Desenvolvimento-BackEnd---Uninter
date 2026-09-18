
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;
namespace Franquias.Api.Controllers
{

   [Authorize(Roles = "Administrador")]
    [ApiController]
     [Route("api/produtos")]
    public class ProdutoServicoController : ControllerBase
    {
        private readonly ProdutoServicoService _service;

        public ProdutoServicoController(ProdutoServicoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(
            string? nome, int? categoriaId, string? status,
            string ordenarPor = "nome", bool decrescente = false,
            int pagina = 1, int tamanhoPagina = 10)
        {
            var produtos = await _service.ListarTodosAsync();

            if (!string.IsNullOrEmpty(nome))
            {
                produtos = produtos.Where(p => p.Nome.Contains(nome)).ToList();
            }
            if (categoriaId.HasValue)
            {
                produtos = produtos.Where(p => p.CategoriaId == categoriaId.Value).ToList();
            }
            if (!string.IsNullOrEmpty(status))
            {
                produtos = produtos.Where(p => p.Status == status).ToList();
            }

            produtos = ordenarPor.ToLower() switch
            {
                "preco" => decrescente ? produtos.OrderByDescending(p => p.PrecoBase).ToList() 
                : produtos.OrderBy(p => p.PrecoBase).ToList(),
                _ => decrescente ? produtos.OrderByDescending(p => p.Nome).ToList() 
                : produtos.OrderBy(p => p.Nome).ToList()
            };

            var total = produtos.Count;
            var itens = produtos.Skip((pagina - 1) * tamanhoPagina).Take(tamanhoPagina).ToList();

            return Ok(new { Total = total, Pagina = pagina, TamanhoPagina = tamanhoPagina, Itens = itens });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var produto = await _service.BuscarPorIdAsync(id);
            if (produto == null)
            {
                return NotFound("Produto/Serviço não encontrado.");
            }
            return Ok(produto);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(ProdutoServicoDTO dto)
        {
            var produto = new ProdutoServico
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                PrecoBase = dto.PrecoBase,
                CategoriaId = dto.CategoriaId,
                FornecedorId = dto.FornecedorId,
                Status = "Ativo"
            };

            await _service.AdicionarAsync(produto);
            var produtoCompleto = await _service.BuscarPorIdAsync(produto.Id);
            return Ok(produtoCompleto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ProdutoServicoDTO dto)
        {
            var produto = await _service.BuscarPorIdAsync(id);
            if (produto == null)
            {
                return NotFound("Produto/Serviço não encontrado.");
            }

            produto.Nome = dto.Nome;
            produto.Descricao = dto.Descricao;
            produto.PrecoBase = dto.PrecoBase;
            produto.CategoriaId = dto.CategoriaId;
            produto.FornecedorId = dto.FornecedorId;

            await _service.AtualizarAsync(produto);
            return Ok(produto);
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> Inativar(int id)
        {
            var produto = await _service.BuscarPorIdAsync(id);
            if (produto == null)
            {
                return NotFound("Produto/Serviço não encontrado.");
            }

            produto.Status = "Inativo";
            await _service.AtualizarAsync(produto);
            return Ok(produto);
        }
    }
}
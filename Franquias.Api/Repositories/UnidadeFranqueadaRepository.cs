
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;
using System.Data.Common;

namespace Franquias.Api.Repositories
{
    public class UnidadeFranqueadaRepository
    {
        private readonly DBFranquias _contexto;

        public UnidadeFranqueadaRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<UnidadeFranqueada>> ListarTodosAsync()
        {
        
         return await _contexto.UnidadesFranqueadas.Include(u => u.Franqueadora).ToListAsync();

        }

        public async Task<UnidadeFranqueada?> BuscarPorIdAsync(int id)
        {
            return await _contexto.UnidadesFranqueadas
        .Include(u => u.Franqueadora)
        .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<UnidadeFranqueada?> BuscarPorCnpjAsync(string cnpj)
        {
            return await _contexto.UnidadesFranqueadas.FirstOrDefaultAsync(u => u.CNPJ == cnpj);
        }

        public async Task AdicionarAsync(UnidadeFranqueada unidade)
        {
            _contexto.UnidadesFranqueadas.Add(unidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(UnidadeFranqueada unidade)
        {
            _contexto.UnidadesFranqueadas.Update(unidade);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(UnidadeFranqueada unidade)
        {
            _contexto.UnidadesFranqueadas.Remove(unidade);
            await _contexto.SaveChangesAsync();
        }
    }
}
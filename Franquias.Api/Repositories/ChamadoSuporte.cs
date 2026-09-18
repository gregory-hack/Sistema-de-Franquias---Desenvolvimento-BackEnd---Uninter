
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class ChamadoSuporteRepository
    {
        private readonly DBFranquias _contexto;

        public ChamadoSuporteRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<ChamadoSuporte>> ListarTodosAsync()
        {
            return await _contexto.ChamadosSuporte
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Include(c => c.Usuario)
                    .ThenInclude(u => u!.Perfil)
                .ToListAsync();
        }

        public async Task<ChamadoSuporte?> BuscarPorIdAsync(int id)
        {
            return await _contexto.ChamadosSuporte
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Include(c => c.Usuario)
                    .ThenInclude(u => u!.Perfil)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<ChamadoSuporte>> ListarPorStatusAsync(string status)
        {
            return await _contexto.ChamadosSuporte
                .Where(c => c.Status == status)
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Include(c => c.Usuario)
                    .ThenInclude(u => u!.Perfil)
                .ToListAsync();
        }

        public async Task<List<ChamadoSuporte>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _contexto.ChamadosSuporte
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Include(c => c.Usuario)
                    .ThenInclude(u => u!.Perfil)
                .Where(c => c.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();
        }

        public async Task AdicionarAsync(ChamadoSuporte chamado)
        {
            _contexto.ChamadosSuporte.Add(chamado);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ChamadoSuporte chamado)
        {
            _contexto.ChamadosSuporte.Update(chamado);
            await _contexto.SaveChangesAsync();
        }
    }
}
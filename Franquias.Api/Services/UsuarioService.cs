
using System.Security.Cryptography;
using System.Text;
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _repositorio;

        public UsuarioService(UsuarioRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Usuario>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<Usuario?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Usuario usuario, string senha)
        {
            var usuarioExistente = await _repositorio.BuscarPorEmailAsync(usuario.Email);
            if (usuarioExistente != null)
            {
                throw new Exception("Já existe um usuário cadastrado com esse e-mail.");
            }

            usuario.SenhaHash = GerarHashSenha(senha);
            await _repositorio.AdicionarAsync(usuario);
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            await _repositorio.AtualizarAsync(usuario);
        }

        public async Task RemoverAsync(Usuario usuario)
        {
            await _repositorio.RemoverAsync(usuario);
        }

        public string GerarHashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool ConferirSenha(string senhaDigitada, string senhaHashSalva)
        {
            var hashDigitado = GerarHashSenha(senhaDigitada);
            return hashDigitado == senhaHashSalva;
        }
    }
}
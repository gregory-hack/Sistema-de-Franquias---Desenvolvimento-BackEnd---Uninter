
using Franquias.Api.Models;
using System.Security.Cryptography;
using System.Text;

namespace Franquias.Api.Data
{
    public static class SeedData
    {
        public static void Popular(DBFranquias contexto)
        {
            if (!contexto.Perfis.Any())
            {
                contexto.Perfis.AddRange(
                    new Perfil { Nome = "Administrador" },
                    new Perfil { Nome = "GestorUnidade" },
                    new Perfil { Nome = "Operador" }
                );

                contexto.SaveChanges();
            }

            if (!contexto.Usuarios.Any())
            {
                var perfilAdmin = contexto.Perfis.First(p => p.Nome == "Administrador");

                contexto.Usuarios.Add(new Usuario
                {
                    Nome = "Administrador",
                    Email = "admin@franquias.com",
                    SenhaHash = GerarHashSenha("admin123"),
                    PerfilId = perfilAdmin.Id,
                    Ativo = true
                });

                contexto.SaveChanges();
            }
        }

        private static string GerarHashSenha(string senha)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(senha);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
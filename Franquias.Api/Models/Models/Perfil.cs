
using System.Reflection.Metadata;

namespace Franquias.Api.Models
{
    public class Perfil
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
    }
}

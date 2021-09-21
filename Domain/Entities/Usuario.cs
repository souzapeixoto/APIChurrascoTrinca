using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class Usuario : IdentityUser
    {
        public Usuario()
        {
            this.RefreshTokens = new HashSet<RefreshToken>();
        }
        public string Nome { get; set; }
        public string Imagem { get; set; }
        public virtual ICollection<Churrasco> Churrascos { get; set; }
        public ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}

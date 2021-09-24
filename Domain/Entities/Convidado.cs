using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Convidado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public virtual Churrasco Churrasco { get; set; }
        public virtual Opcao Opcao { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal ValorContribuicao { get; set; }
    }
}

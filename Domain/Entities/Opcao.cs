using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Opcao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public decimal Valor { get; set; }
        public virtual Churrasco Churrasco { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Churrasco
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Opcao> Opcoes { get; set; }
        public ICollection<Participante> Participantes { get; set; }

        public int TotalDeParticipantes()
        {
            return this.Participantes.Count;
        }

        public decimal ValorArrecadado()
        {
            return this.Participantes.Sum(x=>x.ValorContribuicao);
        }
    }
}

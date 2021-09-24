using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Entities
{
    public class Churrasco
    {
        public Churrasco()
        {
            this.Convidados = new HashSet<Convidado>();
            this.Opcoes = new HashSet<Opcao>();
        }
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public virtual Usuario Usuario { get; set; }
        public virtual ICollection<Opcao> Opcoes { get; set; }
        public ICollection<Convidado> Convidados { get; set; }
        public int TotalDeConvidados { get { return this.Convidados.Count; } }
        public decimal ValorArrecadado { get { return this.Convidados.Sum(x => x.ValorContribuicao); } }

    }
}

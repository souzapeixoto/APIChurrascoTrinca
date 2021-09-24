using System;
using System.Collections.Generic;

namespace Application.DTO
{
    public class DTOChurrasco
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public  ICollection<DTOOpcao> Opcoes { get; set; }
        public ICollection<DTOConvidado> Convidados { get; set; }
        public int TotalDeConvidados { get; set; }

        public decimal ValorArrecadado { get; set; }

    }
}

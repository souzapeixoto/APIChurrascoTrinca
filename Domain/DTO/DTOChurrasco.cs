using System;
using System.Collections.Generic;

namespace Domain.DTO
{
    public class DTOChurrasco
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public  ICollection<DTOOpcao> Opcoes { get; set; }
        public ICollection<DTOParticipante> Participantes { get; set; }
        public int TotalDeParticipantes { get; }

        public decimal ValorArrecadado { get; }
        
    }
}

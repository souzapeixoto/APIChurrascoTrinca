using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIChurrascoTrinca.InputModel
{
    public class ChurrascoCreateInputModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        public ICollection<DTOOpcao> Opcoes { get; set; }
        public ICollection<DTOParticipante> Participantes { get; set; }

    }
    public class Opcao
}

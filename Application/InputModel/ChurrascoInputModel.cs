using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Application.InputModel
{
    public class ChurrascoCreateInputModel
    {
        [Required]
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }
        [Required]
        public ICollection<ChurrascoCreateOpcaoInputModel> Opcoes { get; set; }
        public ICollection<ChurrascoCreateConvidadoInputModel> Convidados { get; set; }

    }

    public class ChurrascoUpdateInputModel
    {
        [Required]
        public DateTime Data { get; set; }
        public string Descricao { get; set; }
        public string Observacoes { get; set; }

    }
    public class ChurrascoCreateOpcaoInputModel
    {
        [Required]
        public string Descricao { get; set; }
        [Required]
        public decimal Valor { get; set; }
    }

    public class ChurrascoCreateConvidadoInputModel
    {
        [Required]
        public string Nome { get; set; }
        public int IdOpcao { get; set; }
        public decimal ValorContribuicao { get; set; }

    }
    public class ChurrascoUpdateConvidadoInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        public int IdOpcao { get; set; }
        public decimal ValorContribuicao { get; set; }

    }

    public class ChurrascoUpdateOpcaoInputModel
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
    }

}

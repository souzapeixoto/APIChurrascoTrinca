﻿namespace Application.DTO
{
    public class DTOConvidado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public  int IdChurrasco { get; set; }
        public  DTOOpcao Opcao { get; set; }
        public decimal ValorContribuicao { get; set; }
    }
}

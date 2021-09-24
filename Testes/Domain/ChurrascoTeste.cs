using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Testes.Domain
{
    public class ChurrascoTeste
    {
        [Fact]
        public void QuantidadeDeConvidados5()
        {
            var churrasco = mockChurrasco();
            Assert.Equal(5, churrasco.TotalDeConvidados);

        }
        [Fact]
        public void ValorArrecadadoParaOChurrasco256_30()
        {
            var churrasco = mockChurrasco();
            Assert.Equal(256.30m, churrasco.ValorArrecadado);

        }

        public static Churrasco mockChurrasco()
        {
            var churrasco1 = new Churrasco();
            churrasco1.Convidados.Add(new Convidado { ValorContribuicao = 80.50m });
            churrasco1.Convidados.Add(new Convidado { ValorContribuicao = 100.00m });
            churrasco1.Convidados.Add(new Convidado { ValorContribuicao = 20m });
            churrasco1.Convidados.Add(new Convidado { ValorContribuicao = 45.30m });
            churrasco1.Convidados.Add(new Convidado { ValorContribuicao = 10.50m });
            return churrasco1;
        }
    }
}

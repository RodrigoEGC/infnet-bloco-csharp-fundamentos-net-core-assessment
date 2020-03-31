using System;
using System.ComponentModel.DataAnnotations;

namespace Dominio
{
    public class Amigo
    {
        public string Nome { get; private set; }

        public string Sobrenome { get; private set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]

        public DateTime DataNascimento { get; set; }

        public Amigo(string nome, string sobrenome, DateTime dataNascimento)
        {
            Nome = nome;
            Sobrenome = sobrenome;
            DataNascimento = dataNascimento;

        }

        public override string ToString()
        {
            return $"{Nome}|{Sobrenome}|{(DateTime)DataNascimento}";
        }

        public string ObterNomeCompleto() => string.Format("{0} {1}", Nome, Sobrenome);

        public int ObterQtdeDeDiasParaOProximoAniversario()
        {
            var dataAniversarioAnoAtual = new DateTime(DateTime.Now.Year, DataNascimento.Month, DataNascimento.Day);
            var qtdeDiasDiff = dataAniversarioAnoAtual.Date - DateTime.Now.Date;
            return qtdeDiasDiff.Days;
        }
    }
}

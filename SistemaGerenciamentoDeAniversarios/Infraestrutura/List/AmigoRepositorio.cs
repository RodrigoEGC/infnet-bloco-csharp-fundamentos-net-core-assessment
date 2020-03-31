using Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Infraestrutura.List
{
    public class AmigoRepositorio : IRepositorio
    {
        private const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
        private static List<Amigo> amigoEncontrado;
        private static Amigo amigoEscolhido;
        private static ushort indexAmigoAExibir;
        private const string DIRETORIO_ARQUIVO = @"C:\arquivoAniversariante\lista_amigo.txt";
        private static List<Amigo> amigoLista = new List<Amigo>();

        public AmigoRepositorio()
        {
            CarregarLista();
        }
        private void CarregarLista()
        {
            if (!File.Exists(DIRETORIO_ARQUIVO))
                File.Create(DIRETORIO_ARQUIVO).Close();

            var linhas = File.ReadAllLines(DIRETORIO_ARQUIVO);

            foreach (var linha in linhas)
            {
                var info = linha.Split("|");

                var sobrenome = info[1];
                var dtNascimento = DateTime.Parse(info[2]);

                var amigo = new Amigo(info[0], sobrenome, dtNascimento);
                amigoLista.Add(amigo);
            }
        }

        public void Adicionar(Amigo amigo)
        {
            amigoLista.Add(amigo);
            File.WriteAllLines(DIRETORIO_ARQUIVO, amigoLista.Select(amigo => amigo.ToString()));
        }
        public void Editar(Amigo amigo)
        {
           File.WriteAllLines(DIRETORIO_ARQUIVO, amigoLista.Select(amigo => amigo.ToString()));
        }

        public void Delete(Amigo amigo)
        {
            amigoLista.Remove(amigo);
            File.WriteAllLines(DIRETORIO_ARQUIVO, amigoLista.Select(amigo => amigo.ToString()));
        }

        public List<Amigo> Pesquisar(string parametroPesquisa)
        {
            return amigoLista.Where(x => x.Nome.ToUpper().Contains(parametroPesquisa.ToUpper()))
                                                         .ToList();
        }
        public void PesquisarAmigo()
        { 
            Console.WriteLine("Informe o nome, ou parte do nome da pessoa que deseja encontrar:");
            var parametroPesquisa = Console.ReadLine();

            amigoEncontrado = Pesquisar(parametroPesquisa);

            if (amigoEncontrado.Count > 0)
            {
                Console.WriteLine("Selecione uma das opções abaixo para visualizar os dados de uma das pessoas encontradas:");
                for (var i = 0; i < amigoEncontrado.Count; i++)
                {
                    Console.WriteLine($"{i} - {amigoEncontrado[i].ObterNomeCompleto()}");
                }

                if (!ushort.TryParse(Console.ReadLine(), out indexAmigoAExibir) || indexAmigoAExibir >= amigoEncontrado.Count)
                {
                    Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
                    Console.ReadKey();
                    return;
                }

                amigoEscolhido = amigoEncontrado[indexAmigoAExibir];

                Console.WriteLine("Dados do(a) amigo(a)");
                Console.WriteLine($"Nome completo: {amigoEscolhido.ObterNomeCompleto()}");
                Console.WriteLine($"Data de nascimento: {amigoEscolhido.DataNascimento:dd/MM/yyyy}");

                Console.WriteLine("-------------------------------------");

                Console.WriteLine("Selecione uma opção digitando o número correspondente ao submenu abaixo:  ");
                Console.WriteLine(" 1 - Editar ");
                Console.WriteLine(" 2 - Deletar ");
                Console.WriteLine(" 3 - Informações sobre o aniversario do(a) amigo(a) : ");
                Console.WriteLine(" 0 - Sair ");

                int opcao = Convert.ToChar(Console.ReadLine());

                switch (opcao)
                {
                    case '1':
                        EditarAmigo();
                        break;
                    case '2':
                        Delete(amigoEscolhido);
                        break;
                    case '3':
                        InformacaoAmigo();
                        break;
                    default:
                        break;
                }

            }
            else
            {
                Console.WriteLine($"Não foi encontrado nenhuma pessoa! {pressioneQualquerTecla}");
                Console.ReadKey();
            }
        }
        private void EditarAmigo()
        {
            if (amigoEncontrado.Count > 0)
            {

                Console.Clear();
                Console.WriteLine("Dados do amigo para edição");
                Console.WriteLine($"Amigo: {amigoEscolhido}");
                Console.WriteLine("-------------------------------------");


                Console.WriteLine("Digite a nova data de nascimento do(a) amigo no(a) formato dd/MM/yyyy ");
                var dtNascimentoEdit = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("A nova data está correta?");
                Console.WriteLine($"Data do Aniversário: {dtNascimentoEdit:dd/MM/yyyy}");
                Console.WriteLine("1 - Sim \n2 - Não");

                var opcaoParaEditar = Console.ReadLine();
                if (opcaoParaEditar == "1")
                {
                    amigoEncontrado[indexAmigoAExibir].DataNascimento = dtNascimentoEdit;
                    Editar(amigoEncontrado[indexAmigoAExibir]);

                    Console.WriteLine($"Data do amigo editada com sucesso! {pressioneQualquerTecla}");
                    Console.ReadKey();
                }
                else if (opcaoParaEditar == "2")
                    Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
                else
                    Console.WriteLine($"Opção inválida! {pressioneQualquerTecla}");
            }

        }

        private void InformacaoAmigo()
        {
            var qtdeDiasParaProxAniversario = amigoEscolhido.ObterQtdeDeDiasParaOProximoAniversario();

            if (qtdeDiasParaProxAniversario > 0)
                Console.Write($"Falta(m) {qtdeDiasParaProxAniversario} dia(s) para esse aniversário. {pressioneQualquerTecla}");

            else if (qtdeDiasParaProxAniversario == 0)
                Console.Write($"Parabéns. Você está fazendo aniversário hoje! {pressioneQualquerTecla} ");
            else
                Console.Write($"Você já fez aniversário neste ano. {pressioneQualquerTecla}");
        }

    }
}

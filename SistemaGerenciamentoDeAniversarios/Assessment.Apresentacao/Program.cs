using Dominio;
using Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Assessment.Apresentacao
{
    class Program
    {
        private const string pressioneQualquerTecla = "Pressione qualquer tecla para exibir o menu principal ...";
        static void Main(string[] args)
        {
            string opcaoEscolhida;

            do
            {
                opcaoEscolhida = ExibirMenuPrincipal();

                if (opcaoEscolhida == "1")
                {
                    AdicionarAmigo();

                }

                else if (opcaoEscolhida == "2")
                {
                    PesquisarAmigos();
                }

                else if (opcaoEscolhida != "3")
                {
                    Console.WriteLine($"Opção inválida. {pressioneQualquerTecla}");
                    Console.ReadKey();
                }


            } while (opcaoEscolhida != "3");

        }

        private static void PesquisarAmigos()
        {
            var repositorioPesquisar = RepositorioFabrica.Criar();
            repositorioPesquisar.PesquisarAmigo();
        }

        private static string ExibirMenuPrincipal()
        {
            Console.Clear();
            Console.WriteLine("--- Gerenciador de Aniversário de Amigos ---\nSelecione uma das opções abaixo:");
            Console.WriteLine(" [ 1 ] Adiciona amigo");
            Console.WriteLine(" [ 2 ] Pesquisar amigos");
            Console.WriteLine(" [ 3 ] Sair");
            Console.WriteLine("-------------------------------------");


            return Console.ReadLine();
        }


        private static void AdicionarAmigo()
        {
            var amigoRepositorio = RepositorioFabrica.Criar();

            Console.WriteLine("Informe o nome da pessoa que deseja adicionar");
            var nomePessoa = Console.ReadLine();

            Console.WriteLine("Informe o sobrenome da pessoa que deseja adicionar");
            var sobreNome = Console.ReadLine();

            Console.WriteLine("Informe a data de nascimento no formato dd/MM/yyyy");
            DateTime dataNascimento;
            if (!DateTime.TryParse(Console.ReadLine(), out dataNascimento))
            {
                Console.WriteLine($"Data inválida! Dados descartados! {pressioneQualquerTecla}");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Os dados estão corretos?");
            Console.WriteLine($"Nome: {nomePessoa} {sobreNome}");
            Console.WriteLine($"Data do Aniversário: {dataNascimento:dd/MM/yyyy}");
            Console.WriteLine("1 - Sim \n2 - Não");
            var opcaoParaAdicionar = Console.ReadLine();

            if (opcaoParaAdicionar == "1")
            {
                var amigo = new Amigo(nomePessoa, sobreNome, dataNascimento);
                amigoRepositorio.Adicionar(amigo);
                Console.WriteLine($"Dados adicionados com sucesso! {pressioneQualquerTecla}");
            }
            else if (opcaoParaAdicionar == "2")
                Console.WriteLine($"Dados descartados! {pressioneQualquerTecla}");
            else
                Console.WriteLine($"Opção inválida. Dados descartados! {pressioneQualquerTecla}");

            Console.ReadKey();
        }
        //private static List<Amigo> AniversarianteDoDia()
        //{
        //    var repositorio = RepositorioFabrica.Criar();
        //    var birthdayList = repositorio.
        //    DateTime todayBirthday = DateTime.Now;

        //    var birthdaysOfDay = birthdayList.Where(p =>
        //        p.DataNascimento.Day == todayBirthday.Day &&
        //        p.DataNascimento.Month == todayBirthday.Month
        //    ).ToList();

        //    return birthdaysOfDay;
        //}

    }
}

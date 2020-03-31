using Dominio;
using System.Collections.Generic;

namespace Infraestrutura
{
    public interface IRepositorio
    {
        List<Amigo> Pesquisar(string parametroPesquisa);
        void Adicionar(Amigo amigo);
        void Delete(Amigo amigo);
        void Editar(Amigo amigo);
        void PesquisarAmigo();
    }
}

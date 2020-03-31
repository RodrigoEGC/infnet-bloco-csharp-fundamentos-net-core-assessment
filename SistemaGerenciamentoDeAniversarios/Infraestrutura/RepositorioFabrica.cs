using System;

namespace Infraestrutura
{
    public class RepositorioFabrica
    {
        public static IRepositorio Criar()
        {
            return Criar(TipoRepositorio.List);
        }

        public static IRepositorio Criar(TipoRepositorio tipoRepositorio)
        {
            switch (tipoRepositorio)
            {
                case TipoRepositorio.List:
                    return new List.AmigoRepositorio();
                //case TipoRepositorio.LinkedList:
                //    return new LinkedList.CarroRepositorio();
                default:
                    throw new NotImplementedException("Não existe repositório padrão!");
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Models.ViewModels
{
    public class BuscaDeProdutoViewModels
    {
        public IList<Produto> Produtos { get; set; }
        public string Pesquisa { get; set; }

        public BuscaDeProdutoViewModels(IList<Produto>  produtos)
        {
            Produtos = produtos;
        }


    }
}

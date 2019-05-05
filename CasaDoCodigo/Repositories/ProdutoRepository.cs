using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        private readonly ICategoriaRepository categoriaRepository;

        public ProdutoRepository(ApplicationContext contexto, ICategoriaRepository categoriaRepository) : base(contexto)
        {
            this.categoriaRepository = categoriaRepository;
        }

        public async Task<IList<Produto>> GetProdutos()
        {
            return await dbSet.Include(p => p.Categoria).ToListAsync();
        }

        public async Task<IList<Produto>> GetProdutos(string search)
        {
            return await dbSet
                            .Where(p => p.Nome.Contains(search) || p.Categoria.Nome.Contains(search))
                            .DefaultIfEmpty()
                            .ToListAsync();
        }

        public async Task SaveProdutos(List<Livro> livros)
        {

            foreach (var livro in livros)
            {
                var categoria = contexto.Set<Categoria>()
                                    .Where(c => c.Nome == livro.Categoria)
                                    .FirstOrDefault();

                if (categoria == null)
                {
                    categoria = new Categoria(livro.Categoria);
                    await categoriaRepository.SaveCategorias(categoria);
                }

                if (!dbSet.Where(p => p.Codigo == livro.Codigo).Any())
                {
                    dbSet.Add(new Produto(livro.Codigo, livro.Nome, categoria, livro.Preco));
                }
            }

            await contexto.SaveChangesAsync();
        }

    }

    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Categoria { get; set; }
        public string Subcategoria { get; set; }
        public decimal Preco { get; set; }
    }
}

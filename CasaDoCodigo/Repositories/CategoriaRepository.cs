using CasaDoCodigo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CasaDoCodigo.Repositories
{
    public interface ICategoriaRepository
    {
        Task SaveCategorias(Categoria categoria);
    }

    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(ApplicationContext contexto) : base(contexto)
        {
        }

        public async Task SaveCategorias(Categoria categoria)
        {
            if (!dbSet.Where(c => c.Nome == categoria.Nome).Any())
            {
                dbSet.Add(categoria);
                await contexto.SaveChangesAsync();
            }

        }

    }
}

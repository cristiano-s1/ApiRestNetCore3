using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SmartSchool.Helpers
{
    public class PageList<T> : List<T> // Umas lista genérica
    {
        // Página atual
        public int CurrentPage { get; set; }

        // Quantidade de itens por página
        public int PageSize { get; set; }

        // Total de Itens
        public int TotalCount { get; set; }

        // Total de páginas
        public int TotalPages { get; set; }
            

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items); // this é a classe PageList
        }

        // Método para trabalhar de forma asincrona
        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
        {
            // await - Esperar retorno do banco de dados
            var count = await source.CountAsync();

            var items = await source.Skip((pageNumber - 1) * pageSize) // Quantidade de páginas
                                    .Take(pageSize) // Tamanho de páginas
                                    .ToListAsync();
            // Retornar a lista
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}

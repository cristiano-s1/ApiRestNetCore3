namespace SmartSchool.Helpers
{
    public class PaginationHeader
    {
        // Página atual
        public int CurrentPage { get; set; }

        // Quantidade de itens por página
        public int ItemsPerPage { get; set; }

        // Total de Itens
        public int TotalItems { get; set; }

        // Total de páginas
        public int TotalPages { get; set; }


        public PaginationHeader(int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            CurrentPage = currentPage;
            ItemsPerPage = itemsPerPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
        }

    }
}

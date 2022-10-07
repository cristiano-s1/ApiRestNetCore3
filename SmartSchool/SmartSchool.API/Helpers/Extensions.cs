using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json.Serialization;

namespace SmartSchool.Helpers
{
    public static class Extensions
    {
        public static void AddPagination(this HttpResponse response,
            int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            var paginationHeader = new PaginationHeader(currentPage, itemsPerPage, totalItems, totalPages);

            // Formatar para camel case (Primeira letra em minusculo)
            // {"currentPage":1,"itemsPerPage":10,"totalItems":9,"totalPages":1}
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();

            response.Headers.Add("Pagination", JsonConvert.SerializeObject(paginationHeader, camelCaseFormatter));
            response.Headers.Add("Acess-Control-Expose-Header", "Pagination");
        }
    }
}

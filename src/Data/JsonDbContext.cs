using System.Linq;

namespace ApiPaginationDemo
{
    public class JsonDbContext
    {
        public IQueryable<Invoice> Invoices { get; set; }
    }
}
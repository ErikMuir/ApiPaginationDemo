using System.Linq;
using ApiPaginationDemo.Models;

namespace ApiPaginationDemo.Data
{
    public class JsonDbContext
    {
        public IQueryable<Invoice> Invoices { get; set; }
    }
}
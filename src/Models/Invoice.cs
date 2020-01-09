using System;

namespace ApiPaginationDemo
{
    public class Invoice
    {
        public int Id { get; set; }
        public Guid CustomerId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }
    }
}

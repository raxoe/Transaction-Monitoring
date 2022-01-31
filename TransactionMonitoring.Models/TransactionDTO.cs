using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionMonitoring.Models
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string TransactionIdentificator { get; set; }
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
        public DateTime TransactionDate { get; set; }
        public string Status { get; set; }
        public string FileType { get; set; }
        public int resultCode { get; set; }
        public string resultDescription { get; set; }
    }
}

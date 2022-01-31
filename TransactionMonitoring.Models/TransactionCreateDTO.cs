using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionMonitoring.Models
{
    public class TransactionCreateDTO
    {
        public List<TransactionDTO> lstTransaction { get; set; }
        public int resultCode { get; set; }
        public string resultDescription { get; set; }
    }
}

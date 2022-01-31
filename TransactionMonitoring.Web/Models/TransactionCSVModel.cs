using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMonitoring.Web.Helpers.CSVHelper;

namespace TransactionMonitoring.Web.Models
{
    public class TransactionCSVModel
    {
        [Name(Constants.CsvHeaders.id)]
        public string id { get; set; }

        [Name(Constants.CsvHeaders.TransactionDate)]
        public string TransactionDate { get; set; }

        [Name(Constants.CsvHeaders.PaymentDetails__Amount)]
        public string PaymentDetails__Amount { get; set; }

        [Name(Constants.CsvHeaders.PaymentDetails__CurrencyCode)]
        public string PaymentDetails__CurrencyCode { get; set; }

        [Name(Constants.CsvHeaders.Status)]
        public string Status { get; set; }
    }
}

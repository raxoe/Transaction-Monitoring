using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionMonitoring.Web.Models;

namespace TransactionMonitoring.Web.Helpers.CSVHelper
{
    public sealed class TransactionMap : ClassMap<TransactionCSVModel>
    {
        public TransactionMap()
        {
            Map(m => m.id).Name(Constants.CsvHeaders.id);
            Map(m => m.TransactionDate).Name(Constants.CsvHeaders.TransactionDate);
            Map(m => m.PaymentDetails__Amount).Name(Constants.CsvHeaders.PaymentDetails__Amount);
            Map(m => m.PaymentDetails__CurrencyCode).Name(Constants.CsvHeaders.PaymentDetails__CurrencyCode);
            Map(m => m.Status).Name(Constants.CsvHeaders.Status);
        }
    }
}

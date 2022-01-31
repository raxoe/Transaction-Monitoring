using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TransactionMonitoring.Web.Helpers.CSVHelper
{
    public static class Constants
    {
        public class CsvHeaders
        {

            public const string id = "id";
            public const string TransactionDate = "TransactionDate";
            public const string PaymentDetails__Amount = "PaymentDetails__Amount";
            public const string PaymentDetails__CurrencyCode = "PaymentDetails__CurrencyCode";
            public const string Status = "Status";
        }
    }
}

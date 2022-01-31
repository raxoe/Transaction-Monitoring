using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TransactionMonitoring.Web.Models
{
    public class MyAttributeProperty
    {
        [XmlAttribute("value")]
        public int Value { get; set; }
    }

    

    public class TransactionInfo
    {
        public List<Transaction> Transactions { get; set; }
    }


    
    public class Transaction
    {
        [XmlAttribute("id")]
        public string id { get; set; }
        public DateTime TransactionDate { get; set; }
        public PaymentDetails PaymentDetails { get; set; }
        public string Status { get; set; }
    }

    public class PaymentDetails
    {
        public decimal Amount { get; set; }
        public string CurrencyCode { get; set; }
    }


    public class Patient
    {
        public List<Measurement> Measurements { get; set; }
    }
    public class Measurement
    {
        public DateTime TimeTaken { get; set; }
        public decimal Temp { get; set; }
    }

}

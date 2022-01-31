using System.Collections.Generic;
using TransactionMonitoring.Web.Models;

namespace TransactionMonitoring.Web.Helpers.CSVHelper.Services
{
    public interface ICsvParserService
    {
        List<TransactionCSVModel> ReadCsvFileToTransactionCSVModel(string path);
        void WriteNewCsvFile(string path, List<TransactionCSVModel> employeeModels);
    }
}
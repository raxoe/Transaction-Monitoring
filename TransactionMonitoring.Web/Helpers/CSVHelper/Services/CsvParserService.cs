using CsvHelper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransactionMonitoring.Web.Models;

namespace TransactionMonitoring.Web.Helpers.CSVHelper.Services
{
    public class CsvParserService : ICsvParserService
    {
        public List<TransactionCSVModel> ReadCsvFileToTransactionCSVModel(string path)
        {
            try
            {
                using (var reader = new StreamReader(path, Encoding.Default))
                using (var csv = new CsvReader(reader))
                {
                    csv.Configuration.RegisterClassMap<TransactionMap>();
                    var records = csv.GetRecords<TransactionCSVModel>().ToList();
                    return records;
                }
            }
            catch (UnauthorizedAccessException e)
            {
                throw new Exception(e.Message);
            }
            catch (FieldValidationException e)
            {
                throw new Exception(e.Message);
            }
            catch (CsvHelperException e)
            {
                throw new Exception(e.Message);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void WriteNewCsvFile(string path, List<TransactionCSVModel> transactionCSVModels)
        {
            using (StreamWriter sw = new StreamWriter(path, false, new UTF8Encoding(true)))
            using (CsvWriter cw = new CsvWriter(sw))
            {
                cw.WriteHeader<TransactionCSVModel>();
                cw.NextRecord();
                foreach (TransactionCSVModel emp in transactionCSVModels)
                {
                    cw.WriteRecord<TransactionCSVModel>(emp);
                    cw.NextRecord();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransactionMonitoring.Models;

namespace TransactionMonitoring.DAL.Interfaces
{
    public interface ITransactionRepository
    {
        Task<TransactionCreateDTO> AddTransactions(TransactionCreateDTO transactionCreateDTO);
        Task<List<TransactionListingDTO>> GetTransactions();
        Task<List<TransactionListingDTO>> GetTransactionByCurrency(string currency);

        Task<List<TransactionListingDTO>> GetTransactionByStatus(string status);

        Task<List<TransactionListingDTO>> GetTransactionByDateRange(string startDate, string endDate);

    }
}

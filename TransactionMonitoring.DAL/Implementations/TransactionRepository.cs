using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using TransactionMonitoring.Logger;
using TransactionMonitoring.Converter;
using TransactionMonitoring.CustomExceptions;
using TransactionMonitoring.DAL.EntityFramework;
using TransactionMonitoring.DAL.Interfaces;
using TransactionMonitoring.EntityModels;
using TransactionMonitoring.Models;
using System.Globalization;

namespace TransactionMonitoring.DAL.Implementations
{
    public class TransactionRepository : ITransactionRepository
    {
        ILoggerHelper _logger = new LoggerHelper();
        private readonly DbTransactionContext _context;

        public TransactionRepository(DbTransactionContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<TransactionCreateDTO> AddTransactions(TransactionCreateDTO transactionCreateDTO)
        {
            try
            {
                try
                {
                    foreach (var transaction in transactionCreateDTO.lstTransaction)
                    {
                        if (await CheckTransactionDuplicate(transaction))
                        {
                            var transactionEntity = new Transaction();
                            TransactionConverter.ConvertModelToEntity(transaction, ref transactionEntity);
                            _context.Transactions.Add(transactionEntity);
                            
                        }
                        else
                        {
                            throw new CustomException(CustomExceptionEnum.TransactionAlreadyExist);
                        }
                    }
                    _context.SaveChanges();
                    transactionCreateDTO.resultCode = (int)CustomExceptionEnum.Success;
                    transactionCreateDTO.resultDescription = CustomException.GetMessage(CustomExceptionEnum.Success);
                }
                catch (CustomException ex)
                {
                    transactionCreateDTO.resultCode = (int)ex.ResultCode;
                    transactionCreateDTO.resultDescription = ex.ResultDescription;
                    _logger.TraceLog(String.Format("Error Code: {0}, Description: {1}", ex.ResultCode, ex.ResultDescription));

                }
                catch (Exception ex)
                {
                    transactionCreateDTO.resultCode = (int)CustomExceptionEnum.UnknownException;
                    transactionCreateDTO.resultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                    _logger.ErrorLog(ex);
                }
                
            }
            catch (Exception ex)
            {
                transactionCreateDTO.resultCode = (int)CustomExceptionEnum.UnknownException;
                transactionCreateDTO.resultDescription = CustomException.GetMessage(CustomExceptionEnum.UnknownException);
                _logger.ErrorLog(ex);
            }
            return await Task.FromResult<TransactionCreateDTO>(transactionCreateDTO);
        }

        public async Task<List<TransactionListingDTO>> GetTransactions()
        {
            List<TransactionListingDTO> transactionListingDTO = new List<TransactionListingDTO>();
            try
            {
                try
                {
                    var transactionEntity = _context.Transactions;

                    foreach (var entity in transactionEntity)
                        transactionListingDTO.Add(TransactionConverter.ConvertEntityToListingModel(entity));

                    _context.SaveChanges();
                }
                catch (CustomException ex)
                {
                    _logger.TraceLog(String.Format("Error Code: {0}, Description: {1}", ex.ResultCode, ex.ResultDescription));

                }
                catch (Exception ex)
                {
                    _logger.ErrorLog(ex);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
            }
            return await Task.FromResult<List<TransactionListingDTO>>(transactionListingDTO);
        }

        public async Task<List<TransactionListingDTO>> GetTransactionByCurrency(string currency)
        {
            List<TransactionListingDTO> transactionListingDTO = new List<TransactionListingDTO>();
            try
            {
                try
                {
                    var transactionEntity = _context.Transactions.Where(t=>t.CurrencyCode.ToLower() == currency.ToLower());

                    foreach (var entity in transactionEntity)
                        transactionListingDTO.Add(TransactionConverter.ConvertEntityToListingModel(entity));

                    _context.SaveChanges();
                }
                catch (CustomException ex)
                {
                    _logger.TraceLog(String.Format("Error Code: {0}, Description: {1}", ex.ResultCode, ex.ResultDescription));

                }
                catch (Exception ex)
                {
                    _logger.ErrorLog(ex);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
            }
            return await Task.FromResult<List<TransactionListingDTO>>(transactionListingDTO);
        }

        public async Task<List<TransactionListingDTO>> GetTransactionByStatus(string status)
        {
            List<TransactionListingDTO> transactionListingDTO = new List<TransactionListingDTO>();
            try
            {
                try
                {
                    var transactionEntity = _context.Transactions.Where(t => t.Status.ToLower() == status.ToLower());

                    foreach (var entity in transactionEntity)
                        transactionListingDTO.Add(TransactionConverter.ConvertEntityToListingModel(entity));

                    _context.SaveChanges();
                }
                catch (CustomException ex)
                {
                    _logger.TraceLog(String.Format("Error Code: {0}, Description: {1}", ex.ResultCode, ex.ResultDescription));

                }
                catch (Exception ex)
                {
                    _logger.ErrorLog(ex);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
            }
            return await Task.FromResult<List<TransactionListingDTO>>(transactionListingDTO);
        }

        public async Task<List<TransactionListingDTO>> GetTransactionByDateRange(string startDate, string endDate)
        {
            
            List<TransactionListingDTO> transactionListingDTO = new List<TransactionListingDTO>();
            try
            {
                try
                {
                    DateTime _startDate = DateTime.ParseExact(startDate, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    DateTime _endDate = DateTime.ParseExact(endDate, "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);

                    var transactionEntity = _context.Transactions.Where(t => t.TransactionDate >= _startDate && t.TransactionDate <= _endDate);

                    foreach (var entity in transactionEntity)
                        transactionListingDTO.Add(TransactionConverter.ConvertEntityToListingModel(entity));

                    _context.SaveChanges();
                }
                catch (CustomException ex)
                {
                    _logger.TraceLog(String.Format("Error Code: {0}, Description: {1}", ex.ResultCode, ex.ResultDescription));

                }
                catch (Exception ex)
                {
                    _logger.ErrorLog(ex);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
            }
            return await Task.FromResult<List<TransactionListingDTO>>(transactionListingDTO);
        }

        private async Task<bool> CheckTransactionDuplicate(TransactionDTO transactionDTO)
        {
            bool result = false;
            if (transactionDTO != null)
            {
                result = (_context.Transactions.Where(x => x.TransactionIdentificator == transactionDTO.TransactionIdentificator).FirstOrDefault() != null) ? false : true;//check duplicate

                //using (var entity = _context)
                //{
                //    result = (entity.Transactions.Where(x => x.TransactionIdentificator != transactionDTO.TransactionIdentificator).FirstOrDefault() != null) ? false : true;//check duplicate
                //}
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.InvalidTransactionInfo);
            }
            return await Task.FromResult<bool>(result);
        }

        
    }
}

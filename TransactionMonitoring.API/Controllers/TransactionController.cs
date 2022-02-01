using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TransactionMonitoring.Common;
using TransactionMonitoring.DAL.Interfaces;
using TransactionMonitoring.Logger;
using TransactionMonitoring.Models;



namespace TransactionMonitoring.API.Controllers
{
    [Route("api/v1/transaction")]
    [ApiController]
    public class TransactionController : BaseController
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentNullException(nameof(transactionRepository));
        }

        [HttpGet]
        public async Task<IActionResult> GetTransaction()
        {
            try
            {
                List<TransactionListingDTO> transactions = await _transactionRepository.GetTransactions();

                if (transactions == null)
                {
                    return NotFound();
                }
                return Ok(transactions);
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("GetTransactionByCurrency/{currency}")]
        public async Task<IActionResult> GetTransactionByCurrency(string currency)
        {
            try
            {
                List<TransactionListingDTO> transaction = await _transactionRepository.GetTransactionByCurrency(currency);

                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpGet("GetTransactionByStatus/{status}")]
        public async Task<IActionResult> GetTransactionByStatus(string status)
        {
            try
            {
                List<TransactionListingDTO> transaction = await _transactionRepository.GetTransactionByStatus(status);

                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }
        
        [HttpGet("GetTransactionByDateInterval")]
        public async Task<IActionResult> GetTransactionByDateInterval(string startDate,string endDate)
        {
            try
            {
                List<TransactionListingDTO> transaction = await _transactionRepository.GetTransactionByDateRange(startDate, endDate);

                if (transaction == null)
                {
                    return NotFound();
                }
                return Ok(transaction);
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionCreateDTO transactionCreateDTO)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(HttpStatusCode.BadRequest.ToString(), ErrorKeys.InvalidInput);
                return BadRequest(ModelState);
            }

            try
            {
                transactionCreateDTO = await _transactionRepository.AddTransactions(transactionCreateDTO);

                if (transactionCreateDTO.resultCode != 0)
                {
                    //return BadRequest(transactionCreateDTO.resultDescription);
                    return Ok(transactionCreateDTO);
                }
            }
            catch (Exception ex)
            {
                _logger.ErrorLog(ex);
                return StatusCode(500, "A problem happened while handling your request");
            }
            return Ok();
        }

    }
}

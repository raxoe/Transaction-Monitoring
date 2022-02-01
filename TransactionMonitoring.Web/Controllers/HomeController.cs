using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;
using TransactionMonitoring.Logger;
using TransactionMonitoring.Models;
using TransactionMonitoring.Web.Helpers.CSVHelper.Services;
using TransactionMonitoring.Web.Models;

namespace TransactionMonitoring.Web.Controllers
{
    public class HomeController : BaseController
    {
        private IWebHostEnvironment _hostingEnvironment;
        private const string relativeURI = "transaction";
        protected readonly ILoggerHelper _logger;

        public HomeController(IConfiguration configuration, IWebHostEnvironment environment, ILoggerHelper logger) : base(configuration)
        {
            _hostingEnvironment = environment;
            _logger = new LoggerHelper();

        }

        public async Task<IActionResult> Index()
        {
            try
            {
                string endpoint = relativeURI;

                var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    endpoint));
                var result = await GetAsync<List<TransactionListingDTO>>(requestUrl);
                return View(result);
            }
            catch(Exception ex)
            {
                _logger.ErrorLog(ex);
            }
            return View();
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreateViewModel transactionCreateViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ViewBag.error = ModelState.Select(x => x.Value.Errors)
                           .FirstOrDefault(y => y.Count > 0).First().ErrorMessage;
                    return View();
                }

                IFormFile postedFile = transactionCreateViewModel.formFile;
                if (postedFile != null)
                {
                    string endpoint = relativeURI;

                    var path = Path.Combine(_hostingEnvironment.WebRootPath, "upload");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }


                    string fileName = Path.GetFileName(postedFile.FileName);
                    string filePath = Path.Combine(path, fileName);
                    string fileExtension = Path.GetExtension(postedFile.FileName).Replace(".", "");

                    TransactionCreateDTO transactionCreateDTO = new TransactionCreateDTO();
                    transactionCreateDTO.lstTransaction = new List<TransactionDTO>();

                    #region CSV
                    if (fileExtension.ToLower() == "csv")
                    {


                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }


                        var csvParserService = new CsvParserService();
                        var lstCSVResults = csvParserService.ReadCsvFileToTransactionCSVModel(filePath);



                        if (lstCSVResults.Count() != 0)
                        {

                            //string endpoint = apiBaseUrl + relativeURI;




                            foreach (var transaction in lstCSVResults)
                            {
                                transactionCreateDTO.lstTransaction.Add(new TransactionDTO()
                                {
                                    TransactionIdentificator = transaction.id,
                                    TransactionDate = Convert.ToDateTime(transaction.TransactionDate),
                                    Amount = Convert.ToDecimal(transaction.PaymentDetails__Amount),
                                    CurrencyCode = transaction.PaymentDetails__CurrencyCode,
                                    Status = transaction.Status,
                                    FileType = fileExtension
                                }); ;
                            }
                        }

                    }
                    #endregion

                    #region XML
                    if (fileExtension.ToLower() == "xml")
                    {
                        using (FileStream stream = new FileStream(filePath, FileMode.Create))
                        {
                            postedFile.CopyTo(stream);
                        }

                        var serializer = new XmlSerializer(typeof(TransactionInfo));
                        //var lstXMLResults = serializer.Deserialize(postedFile.OpenReadStream());

                        TransactionInfo xmlResult = (TransactionInfo)serializer.Deserialize(postedFile.OpenReadStream());

                        if (xmlResult.Transactions.Count() != 0)
                        {

                            //string endpoint = apiBaseUrl + relativeURI;




                            foreach (var transaction in xmlResult.Transactions)
                            {
                                transactionCreateDTO.lstTransaction.Add(new TransactionDTO()
                                {
                                    TransactionIdentificator = transaction.id,
                                    TransactionDate = Convert.ToDateTime(transaction.TransactionDate),
                                    Amount = Convert.ToDecimal(transaction.PaymentDetails.Amount),
                                    CurrencyCode = transaction.PaymentDetails.CurrencyCode,
                                    Status = transaction.Status,
                                    FileType = fileExtension
                                }); ;
                            }
                        }
                    }
                    #endregion

                    var requestUrl = CreateRequestUri(string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    endpoint));
                    var result = await PostAsync<TransactionCreateDTO>(requestUrl, transactionCreateDTO);
                    if (result.resultCode != 0)
                    {
                        ViewBag.error = result.resultDescription;
                        return View();
                    }

                }

                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
            catch (Exception ex)
            {
                ViewBag.error = "Something went wrong. Please try again!";
                _logger.ErrorLog(ex);
            }

            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

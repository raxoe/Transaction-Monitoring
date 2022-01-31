using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TransactionMonitoring.Common.Enums;
using TransactionMonitoring.CustomExceptions;
using TransactionMonitoring.EntityModels;
using TransactionMonitoring.Models;

namespace TransactionMonitoring.Converter
{
    public static class TransactionConverter
    {
        public static void ConvertModelToEntity(TransactionDTO model, ref Transaction entity)
        {
            if (model == null)
                throw new CustomException(CustomExceptionEnum.InvalidTransaction);

            if (model.TransactionIdentificator == null)
                throw new CustomException(CustomExceptionEnum.RequiredTransactionIdentificator);

            if (model.TransactionIdentificator.Length > 50)
                throw new CustomException(CustomExceptionEnum.TransactionIdentificatorTooLong);

            if (model.CurrencyCode == null)
                throw new CustomException(CustomExceptionEnum.RequiredCurrencyCode);

            if (model.CurrencyCode.Length > 3)
                throw new CustomException(CustomExceptionEnum.CurrencyCodeTooLong);

            if (model.TransactionDate == null || DateTime.Compare(DateTime.ParseExact(model.TransactionDate.ToString("dd/MM/yyyy hh:mm:ss"), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture), default(DateTime)) == 0)
                throw new CustomException(CustomExceptionEnum.InvalidTransactionDate);

            if (model.Status == null)
                throw new CustomException(CustomExceptionEnum.InvalidTransactionStatus);

            if (model.FileType.ToLower() == "xml" && !Enum.IsDefined(typeof(TransactionXMLStatusEnum), model.Status))
                throw new CustomException(CustomExceptionEnum.InvalidTransactionStatus);

            if (model.FileType.ToLower() == "csv" && !Enum.IsDefined(typeof(TransactionCSVStatusEnum), model.Status))
                throw new CustomException(CustomExceptionEnum.InvalidTransactionStatus);

            entity.Id = model.Id;
            entity.TransactionIdentificator = model.TransactionIdentificator;
            entity.Amount = model.Amount;
            entity.CurrencyCode = model.CurrencyCode;
            entity.TransactionDate = model.TransactionDate;

            if (model.FileType == "csv")
            {
                if (model.Status == "Approved")
                    entity.Status = "A";
                if (model.Status == "Failed")
                    entity.Status = "R";
                if (model.Status == "Failed")
                    entity.Status = "D";
            }
            else if (model.FileType == "xml")
            {
                if (model.Status == "Approved")
                    entity.Status = "A";
                if (model.Status == "Rejected")
                    entity.Status = "R";
                if (model.Status == "Done")
                    entity.Status = "D";
            }

            return;
        }

        public static TransactionDTO ConvertEntityToModel(Transaction entity)
        {
            var model = new TransactionDTO();
            if (entity != null)
            {
                model.Id = entity.Id;
                model.TransactionIdentificator = entity.TransactionIdentificator;
                model.Amount = entity.Amount;
                model.CurrencyCode = entity.CurrencyCode;
                model.TransactionDate = entity.TransactionDate;
                model.Status = entity.Status;
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.InvalidTransactionInfo);
            }

            return model;
        }

        public static TransactionListingDTO ConvertEntityToListingModel(Transaction entity)
        {
            var model = new TransactionListingDTO();
            if (entity != null)
            {
                model.id = entity.TransactionIdentificator;
                model.payment = entity.Amount + " " + entity.CurrencyCode;
                model.Status = entity.Status;
            }
            else
            {
                throw new CustomException(CustomExceptionEnum.InvalidTransactionInfo);
            }

            return model;
        }
    }
}

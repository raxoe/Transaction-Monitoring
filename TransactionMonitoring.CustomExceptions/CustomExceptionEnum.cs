using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionMonitoring.CustomExceptions
{
    public enum CustomExceptionEnum
    {
        Success,
        UnknownException,
        NoTransactionAvaliable,
        TransactionAlreadyExist,
        TransactionIdentificatorTooLong,
        InvalidTransaction,
        RequiredTransactionIdentificator,
        RequiredCurrencyCode,
        CurrencyCodeTooLong,
        InvalidTransactionDate,
        InvalidTransactionStatus,
        InvalidTransactionInfo
    }
}

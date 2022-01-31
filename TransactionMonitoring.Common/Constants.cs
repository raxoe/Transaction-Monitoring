using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionMonitoring.Common
{
    public class ErrorKeys
    {
        public const string InvalidInput = "InvalidInput";
        public const string ServerError = "ServerError";
        public const string DependecyNotFound = "DependecyNotFound";
        public const string EntityNotFound = "EntityNotFound";
        public const string MultipleEntityFound = "MultipleEntityFound";
        public const string EntityExists = "EntityExists";
        public const string AccessDenied = "AccessDenied";
        public const string ServiceNotAssigned = "ServiceNotAssigned";
        public const string EntityAlreadyUpdated = "EntityAlreadyUpdated";
        public const string PrinterNotAssigned = "PrinterNotAssigned";
        public const string DisplayNotMapped = "DisplayNotMapped";
        public const string ServiceConflictForPrintTicket = "ServiceConflictForPrintTicket";

    }
}

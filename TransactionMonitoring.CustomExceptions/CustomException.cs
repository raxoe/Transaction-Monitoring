using System;
using System.Collections.Generic;
using System.Text;

namespace TransactionMonitoring.CustomExceptions
{
    [Serializable]
    public class CustomException : Exception
    {
        private CustomExceptionEnum resultCode;
        private String resultDescription;

        public CustomException()
        { }

        public CustomException(CustomExceptionEnum message)
            : base(message.ToString())
        {
            resultCode = message;
            resultDescription = CustomExceptions.ResourceManager.GetString(message.ToString());
        }

        public CustomException(CustomExceptionEnum message, string paramenter, bool isPrefix)
            : base(message.ToString())
        {
            if (isPrefix)
            {
                resultDescription = paramenter + " " + CustomExceptions.ResourceManager.GetString(message.ToString());
            }
            else
            {
                resultDescription = CustomExceptions.ResourceManager.GetString(message.ToString()) + " " + paramenter;
            }
            resultCode = message;
        }

        public CustomException(CustomExceptionEnum message, Exception innerException)
            : base(message.ToString(), innerException)
        {
            resultCode = message;
            resultDescription = CustomExceptions.ResourceManager.GetString(message.ToString());
        }

        public CustomExceptionEnum ResultCode
        {
            get { return resultCode; }
        }
        public String ResultDescription
        {
            get { return resultDescription; }
        }


        public static string GetMessage(CustomExceptionEnum message)
        {
            return CustomExceptions.ResourceManager.GetString(message.ToString());
        }
    }
}

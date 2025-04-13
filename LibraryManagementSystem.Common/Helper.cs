using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Common
{
    public static class Helper
    {
        public static (string, string) GetMessage(string className, string generalMessage, Exception? innerException)
        {
            bool hasInnerMessage = innerException != null && innerException.Message != null;
            string message = hasInnerMessage ? innerException?.Message??"" : generalMessage;

            if (!hasInnerMessage)
            {
                return (message, string.Empty);
            }

            string clientMessage = "";
            string field = "";

            if (innerException is SqlException sqlEx)
            {
                field = GetFieldName(message);

                switch (sqlEx.Number)
                {
                    case 515:
                        clientMessage = $"The {field} field is required.";
                        break;

                    case 547:
                        clientMessage = $"The value entered into the {field} field is invalid.";
                        break;
                }

                if (GetTableName(message) != GetMainTableName(className))
                {
                    // When the validation is in a secundary table,
                    // don't show the message in an specific field.
                    field = "";
                }
            }

            return (clientMessage, field);
        }

        private static string GetTableName(string message)
        {
            Match match = Regex.Match(message, @"table\s+""dbo\.(\w+)""", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                return match.Groups[1].Value.ToLower();
            }

            return "";
        }

        private static string GetFieldName(string message)
        {
            Match match = Regex.Match(message, @"column '(\w+)'");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return "";
        }

        private static string GetMainTableName(string className)
        {
            className = className.ToLower();

            if (className.EndsWith("service"))
            {
                className = className.Substring(0, className.Length - "service".Length);
            }

            if (!className.EndsWith("s"))
            {
                className = $"{className}s";
            }

            return className;
        }
    }
}

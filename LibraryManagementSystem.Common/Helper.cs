using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Common
{
    public static class Helper
    {
        public static (string, string) GetMessage(string className, string genericMessage, Exception? innerException)
        {
            bool hasInnerMessage = innerException != null && innerException.Message != null;
            string message = hasInnerMessage ? innerException?.Message ?? "" : genericMessage;

            if (!hasInnerMessage)
            {
                return (GetGenericMessage(message), string.Empty);
            }

            return GetSqlServerMessage(className, message, innerException);
        }

        private static string GetGenericMessage(string message)
        {
            // EF Errors.
            Match match = Regex.Match(message, @"The association between entity types '(\w+)' and '(\w+)'", RegexOptions.IgnoreCase);

            if (match.Success)
            {
                string entity1 = match.Groups[1].Value.ToLower();
                string entity2 = match.Groups[2].Value.ToLower();

                return $"Invalid operation. This {entity1} is already associated with at least one {entity2}.";
            }

            // Generic Errors.
            return message;
        }

        private static (string, string) GetSqlServerMessage(string className, string message, Exception? innerException)
        {
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

                    case 2601:
                        clientMessage = $"Duplicated data not valid ({GetDuplicatedKey(message)}).";
                        break;
                }

                if (field != "" && GetTableName(message) != GetMainTableName(className))
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

        private static string GetDuplicatedKey(string message)
        {
            Match match = Regex.Match(message, @"The duplicate key value is \((.*?)\)");

            if (match.Success)
            {
                return match.Groups[1].Value;
            }

            return "";
        }
    }
}

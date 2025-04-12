using System.Text.RegularExpressions;
using Microsoft.Data.SqlClient;

namespace LibraryManagementSystem.Common
{
    public static class Issue
    {
        public static (string, string) GetMessage(string generalMessage, Exception? innerException)
        {
            bool hasInnerMessage = innerException != null && innerException.Message != null;
            string message = hasInnerMessage ? innerException?.Message??"" : generalMessage;

            if (!hasInnerMessage)
            {
                return (message, string.Empty);
            }

            string clientMessage = string.Empty;
            string field = string.Empty;

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
            }

            return (clientMessage, field);
        }

        private static string GetFieldName(string message)
        {
            Match match = Regex.Match(message, @"column '(\w+)'");

            if (match.Success)
            {
                string columnName = match.Groups[1].Value;
                return columnName;
            }

            return "";
        }
    }
}

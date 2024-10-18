namespace Shared.Modules
{
    public static class DateParser
    {
        public static DateTime? ParseDate(string dateString)
        {
            // Use DateTime.TryParse to handle various date formats
            if (DateTime.TryParse(dateString, out var parsedDate))
            {
                return parsedDate; // Return successfully parsed date
            }

            // If parsing fails, return null
            return null;
        }
    }
}

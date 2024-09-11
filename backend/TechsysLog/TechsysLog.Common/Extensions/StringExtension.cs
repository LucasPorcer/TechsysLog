namespace TechsysLog.Common.Extensions
{
    public static class StringExtensions
    {
        public static string ToBase64(this string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return string.Empty;

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        
        public static string FromBase64(this string base64EncodedData)
        {
            if (string.IsNullOrEmpty(base64EncodedData))
                return string.Empty;

            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }

}

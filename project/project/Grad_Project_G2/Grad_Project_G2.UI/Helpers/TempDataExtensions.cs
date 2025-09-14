using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Grad_Project_G2.UI.Helpers
{
    public static class TempDataExtensions
    {
        private const string TempDataKey = "TempDataMessage";

        public static void SetSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[TempDataKey] = $"success|{message}";
        }

        public static void SetErrorMessage(this ITempDataDictionary tempData, string message)
        {
            tempData[TempDataKey] = $"danger|{message}";
        }

        public static (string Type, string Message)? GetMessage(this ITempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(TempDataKey)) return null;

            var value = tempData[TempDataKey]?.ToString();
            if (string.IsNullOrEmpty(value)) return null;

            var parts = value.Split('|', 2);
            return (parts[0], parts[1]);
        }
    }
}

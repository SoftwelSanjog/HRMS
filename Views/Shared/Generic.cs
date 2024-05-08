namespace HRMS.Views.Shared
{
    public class Generic
    {
        public static string GetFileExtension(string contentType)
        {
            // Define a mapping between content types and file extensions
            Dictionary<string, string> contentTypeMappings = new Dictionary<string, string>
        {
            {"image/jpeg", ".jpg"},
            {"image/png", ".png"},
            {"application/pdf", ".pdf"},
            // Add more mappings as needed
        };

            // Check if the content type exists in the mapping
            if (contentTypeMappings.ContainsKey(contentType))
            {
                // Return the associated file extension
                return contentTypeMappings[contentType];
            }
            else
            {
                // If content type not found, return empty string or handle it accordingly
                return string.Empty;
            }
        }
    }
  
}

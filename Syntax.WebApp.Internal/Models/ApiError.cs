using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Syntax.WebApp.Internal.Models
{
    public class ApiError
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public int Status { get; set; }
        public string TraceId { get; set; }

        public JsonElement Errors { get; set; }

        public List<string> ErrorMessages
        {
            get
            {
                var errorMessages = new List<string>();

                if (Errors.ValueKind == JsonValueKind.Array)
                {
                    foreach (var error in Errors.EnumerateArray())
                    {
                        errorMessages.Add(error.GetString());
                    }
                }
                else if (Errors.ValueKind == JsonValueKind.Object)
                {
                    var errorDictionary = JsonSerializer.Deserialize<Dictionary<string, string[]>>(Errors.GetRawText());
                    foreach (var error in errorDictionary)
                    {
                        foreach (var message in error.Value)
                        {
                            var errorMessageWithTitle = $"{error.Key}: {message}";
                            errorMessages.Add(errorMessageWithTitle);
                        }
                    }
                }

                return errorMessages;
            }
        }
    }
}

using System.Text.Json;
using WebAPI.models;

namespace WebAPI.Services
{
    public class FileService
    {
        private readonly string _surveyFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "surveys.json");
        private readonly string _responseFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "responses.json");

        public FileService()
        {
            string directoryPath = Path.GetDirectoryName(_surveyFilePath);
            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            if (!File.Exists(_surveyFilePath))
            {
                File.WriteAllText(_surveyFilePath, "[]"); // Create an empty JSON array
            }
            if (!File.Exists(_responseFilePath))
            {
                File.WriteAllText(_responseFilePath, "[]"); // Create an empty JSON array
            }
        }

        public List<Survey> LoadSurveys()
        {
            try
            {
                var jsonData = File.ReadAllText(_surveyFilePath);
                return JsonSerializer.Deserialize<List<Survey>>(jsonData) ?? new List<Survey>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading surveys: {ex.Message}");
                return new List<Survey>();
            }
        }

        public void SaveSurveys(List<Survey> surveys)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(surveys, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_surveyFilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving surveys: {ex.Message}");
            }
        }

        public List<SurveyResponse> LoadResponses()
        {
            try
            {
                var jsonData = File.ReadAllText(_responseFilePath);
                return JsonSerializer.Deserialize<List<SurveyResponse>>(jsonData) ?? new List<SurveyResponse>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading responses: {ex.Message}");
                return new List<SurveyResponse>();
            }
        }

        public void SaveResponses(List<SurveyResponse> responses)
        {
            try
            {
                var jsonData = JsonSerializer.Serialize(responses, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(_responseFilePath, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving responses: {ex.Message}");
            }
        }
    }
}

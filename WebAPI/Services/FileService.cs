using System.Text.Json;
using WebAPI.Models;

namespace WebAPI.Services
{
    public class FileService
    {
        private readonly string _dataDirectory = "Data";
        private readonly string _surveyFilePath = "Data/surveys.json";
        private readonly string _responseFilePath = "Data/responses.json";

        public FileService()
        {
            _surveyFilePath = Path.Combine(_dataDirectory, "surveys.json");
            _responseFilePath = Path.Combine(_dataDirectory, "responses.json");

            // Ensure the Data directory exists
            if (!Directory.Exists(_dataDirectory))
            {
                Directory.CreateDirectory(_dataDirectory);
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
            var jsonData = File.ReadAllText(_surveyFilePath);
            return JsonSerializer.Deserialize<List<Survey>>(jsonData) ?? new List<Survey>();
        }

        public void SaveSurveys(List<Survey> surveys)
        {
            var jsonData = JsonSerializer.Serialize(surveys, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_surveyFilePath, jsonData);
        }

        // Response Methods
        public List<SurveyResponse> LoadResponses()
        {
            var jsonData = File.ReadAllText(_responseFilePath);
            return JsonSerializer.Deserialize<List<SurveyResponse>>(jsonData) ?? new List<SurveyResponse>();
        }

        public void SaveResponses(List<SurveyResponse> responses)
        {
            var jsonData = JsonSerializer.Serialize(responses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_responseFilePath, jsonData);
        }
    }
}
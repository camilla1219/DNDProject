# Web service design & Implementation

This document outlines how the Web service design and how it was implemented into our project.

## Overview

This project involves a Restful API designed for Creating and managing surveys and the responses to these surveys. The system has 4 different models Option, Question, Response and Survey models. These are used to structure the data, meanwhile the system also has 2 controllers for handling the endpoints for surveys and responses. Also it includes a fileservice file which handles saving to a dedicated data folder.

## Working with RESTful Web API

Our system has 4 different models and 2 controllers.

### Question Model

The question model has a unique Id to identify each individual question, while also having a string text and string type. The string text is used to write what the question will be, while the string type is used to see what type of question it is e.g MultipleChoice.
Furthermore it has a list of options, which are the different answers to the question.

```csharp
namespace WebAPI.Models
{
    public class Question
    {
        public int Id {get; set; }

        public required string Text {get; set; }

         public required string Type { get; set; }


        public List<Option> Options {get; set; } = new List<Option>();

    }
}

```

### Option Model

The option model has a unique Id assigned to each option, and a public string text which defines what this option is.
The option is an answer to a question.

```csharp
namespace WebAPI.Models
{
    public class Option
    {
        public int Id { get; set; } // Unique identifier for the option
        public string Text { get; set; } = ""; // The option text itself
    }
}

```

### Survey Model

The survey model has a Id to have a unique identifier for each survey, a title for the survey and a description. These two are used to describe what the creator wants respondents to respond to. The Survey model also takes a list of questions, which will be the questions in the survey.

```csharp
namespace WebAPI.Models
{
    public class Survey
    {
        public int Id {get; set; }
        public required string Title {get; set; }

        public required string Description {get; set; }

        public List<Question> Questions {get; set; } = new List<Question>();

    }
}

```

### Response Model

The response model has a Id as a unique identifier for each survey, it also takes a SurveyId to identify which survey is being answered. Furthermore the response model includes respondentName to know who is responding to the survey, and a datetime SubmittedAt to know when they responded. The model also takes a list of ResponseAnswer's which is a list of the answers to the survey, these would be the QuestionId and the SelectedOption. The SelectedOption is equal to the answer which the respondent selected.

```csharp
namespace WebAPI.Models
{
    public class SurveyResponse
    {
        public int Id {get; set; }
        public int SurveyId {get; set; }

        public required string RespondentName {get; set; }

        public DateTime SubmittedAt {get; set; }

        public List<ResponseAnswer> Answers {get; set; } = new List<ResponseAnswer>();

    }

    public class ResponseAnswer
    {
        public int QuestionId {get; set; }
        public required string SelectedOption {get; set; }
    }
}

```

### Controllers

We have a survey Controller and a response Controller to handle the endpoints for individually survey and response.

Both controllers route to api/[controller] and are linked to the fileservice file for data storage.

```csharp
[Route("api/[controller]")]
```

### Survey Controller

The survey controller handles the different endpoints for surveys

### Code examples

```csharp
[HttpGet]
        public ActionResult<List<Survey>> GetSurveys()
        {
            return Ok(_surveys);
        }

        [HttpGet("{Id}")]
        public ActionResult<Survey> GetSurvey(int Id)
        {
            var survey = _surveys.FirstOrDefault(s => s.Id == Id);
            if (survey == null) return NotFound();
            return Ok(survey);
        }
```

### Response Controller

The response controller handles the endpoints for the responses

### Code examples

```csharp
[HttpPost]
    public ActionResult AddResponse(SurveyResponse newResponse)
    {
        newResponse.Id = _responses.Any() ? _responses.Max(r => r.Id) + 1 : 1;
        newResponse.SubmittedAt = DateTime.UtcNow;
        _responses.Add(newResponse);
        _fileService.SaveResponses(_responses);
        return CreatedAtAction(nameof(GetResponse), new { id = newResponse.Id }, newResponse);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteResponse(int id)
    {
        var response = _responses.FirstOrDefault(r => r.Id == id);
        if (response == null) return NotFound();
        _responses.Remove(response);
        _fileService.SaveResponses(_responses);
        return NoContent();
    }
```

## API Endpoints

### Survey Controller Endpoints

### **Get All Surveys**

- **Endpoint**: `GET /api/survey`
- **Description**: Retrieves all surveys.
- **Response**:
  - **200 OK**: Returns a list of all surveys.

### **Get a Single Survey**

- **Endpoint**: `GET /api/survey/{id}`
- **Description**: Retrieves a specific survey by its ID.
- **Parameters**:
  - `id` (int): The ID of the survey to retrieve.
- **Response**:
  - **200 OK**: Returns the survey.
  - **404 Not Found**: If no survey with the given ID exists.

### **Add a New Survey**

- **Endpoint**: `POST /api/survey`
- **Description**: Creates a new survey and assigns unique IDs to its questions and options.
- **Request Body**:
  ```json
  {
    "title": "Survey Title",
    "description": "Survey Description",
    "questions": [
      {
        "text": "Question Text",
        "options": [{ "text": "Option 1" }, { "text": "Option 2" }]
      }
    ]
  }
  ```
  - **Response**:
  - **201 Created**: Returns the created survey with its assigned ID.

### **Update an Existing Survey**

- **Endpoint**: `PUT /api/survey/{id}`
- **Description**: Updates the details of an existing survey.
- **Parameters**:
  - `id` (int): The ID of the survey to update.
- **Request Body**:
  ```json
  {
    "title": "Updated Title",
    "description": "Updated Description",
    "questions": [
      {
        "text": "Updated Question Text",
        "options": [
          { "text": "Updated Option 1" },
          { "text": "Updated Option 2" }
        ]
      }
    ]
  }
  ```
- **Response**:
  - **204 No Content**: If the update is successful.
  - **404 Not Found**: If no survey with the given ID exists.

### **Delete a Survey**

- **Endpoint**: `DELETE /api/survey/{id}`
- **Description**: Deletes a specific survey by its ID.
- **Parameters**:
  - `id` (int): The ID of the survey to delete.
- **Response**:
  - **204 No Content**: If the deletion is successful.
  - **404 Not Found**: If no survey with the given ID exists.

---

### Response Controller Endpoints

### **Get All Survey Responses**

- **Endpoint**: `GET /api/surveyresponse`
- **Description**: Retrieves all survey responses.
- **Response**:
  - **200 OK**: Returns a list of all responses.

### **Get a Single Survey Response**

- **Endpoint**: `GET /api/surveyresponse/{id}`
- **Description**: Retrieves a specific survey response by its ID.
- **Parameters**:
  - `id` (int): The ID of the response to retrieve.
- **Response**:
  - **200 OK**: Returns the response.
  - **404 Not Found**: If no response with the given ID exists.

### **Add a New Survey Response**

- **Endpoint**: `POST /api/surveyresponse`
- **Description**: Submits a new response for a survey.
- **Request Body**:
  ```json
  {
    "surveyId": 1,
    "selectedOptionIds": [1, 2]
  }
  ```
- **Response**:
  - **201 Created**: Returns the created response with its assigned ID and submission timestamp.

### **Delete a Survey Response**

- **Endpoint**: `DELETE /api/surveyresponse/{id}`
- **Description**: Deletes a specific survey response by its ID.
- **Parameters**:
  - `id` (int): The ID of the response to delete.
- **Response**:
  - **204 No Content**: If the deletion is successful.
  - **404 Not Found**: If no response with the given ID exists.

---

### Extra notes

- Each endpoint returns proper HTTP status codes to indicate success or failure.
- Survey IDs, question IDs, and option IDs are automatically managed by the system during creation.

## Filestorage

Filestorage is implemented through a file called fileservice, it saves the files in a directory called Data, and within the data folder it creates two seperate JSON files that contain seperately the Responses.json and Survey.json data.

```csharp
        private readonly string _dataDirectory = "Data";
        private readonly string _surveyFilePath = "Data/surveys.json";
        private readonly string _responseFilePath = "Data/responses.json";

            _surveyFilePath = Path.Combine(_dataDirectory, "surveys.json");
         _responseFilePath = Path.Combine(_dataDirectory, "responses.json");
```

The fileservice files checks if any .json files exist in the directory and if they dont, then it creates a file

```csharp
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
```

Lastly the fileservice file is able to load and save the surveys from the individual json files.

### Load/save Surveys

```csharp

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

```

### Load/save Response

```csharp

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

```

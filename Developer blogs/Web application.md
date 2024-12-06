# Web Application

This section provides an in-depth look at the structure, functionality, and implementation of our survey platform, highlighting its design, integration, and core features. The application is built using Blazor and leverages a seamless connection with a WebAPI to deliver a robust, user-friendly experience. We will explore three key aspects of the application:

Overview of the Pages in Our Web Application: A detailed walkthrough of the main pages, showcasing their purpose and functionality, including survey creation, answering, and response viewing.

Overview of the Connection Between the Blazor App and WebAPI: An explanation of how the Blazor front-end communicates with the WebAPI backend, focusing on the role of shared services and models.

Key Requirements Implementation in the Web Application: A demonstration of how core requirements, such as survey creation, response collection, and result analysis, are addressed in the application’s design and features.

Through these sections, we aim to provide a comprehensive understanding of the application’s architecture, functionality, and the thoughtful design choices that underpin its usability and effectiveness.

## Overview of the pages in our web application

This web application is a survey platform designed to allow users to create, answer, and view surveys. It is built using Blazor components and follows a responsive design to ensure compatibility across different devices. The key features include:

- **Home Page**: Displays a welcome message and user authentication status.
- **Survey Creation**: Users can create surveys by defining a title, description, and questions.
- **Answer Surveys**: Users can answer surveys by providing responses to multiple-choice, checkbox, and short-answer questions.
- **Survey Details**: Allows users to view detailed survey information.
- **View Responses**: Users can view responses submitted to specific surveys.
- **Error Handling**: Provides an error page when something goes wrong in the application.

### 1. Home.razor

**Purpose**: The home page serves as a welcome screen with user authentication status and basic app details.

**What it does**:

- **App Greeting**: Displays a welcome message, introducing the DND Blazor App.
- **Authentication Status**:
  - If the user is logged in, it shows their name and a logout link.
  - If the user is not logged in, it provides a link to the login page.
- **Check Account Button**: For logged-in users, it provides a button to display a personalized greeting via an alert.
- **Responsive Layout**: The layout uses Bootstrap’s grid system to ensure a clean and responsive design for various screen sizes.

### 2. CreateSurvey.razor

**Purpose**: Allows users to create a new survey by providing a title, description, and questions.

**What it does**:

- **Title and Description Input**: Text fields for entering survey title and description.
- **Question Management**: Add and remove questions with different types MultipleChoice, Checkboxes, ShortAnswer.
- **Option Management**: Add/remove options for multiple choice and checkbox questions.
- **Create Button**: Submit the survey after completing all the required fields.
- **Delete Button**: Allows to delete question completely or just delete options

### 3. AnswerSurvey(main).razor

**Purpose**: Displays the survey and allows users to submit their responses.

**What it does**:

- **Survey Details**: Shows the survey title and description.
- **Questions and Answer Options**: Displays each question with its corresponding input field such as radio buttons for multiple choice, checkboxes, text input for short answers.
- **Respondent Name**: Input field to collect the respondent's name.
- **Submit Button**: Allows users to submit their answers.
- **Related Page**: **AnswerSurveyMain.razor**
  - **Survey ID Input**: Allows users to enter the Survey ID and navigate to the respective survey page.
  - **Go Button**: Navigates to the survey page to answer the survey.

### 4. MySurvey(main).razor

**Purpose**: Displays detailed information about a specific survey.

**What it does**:

- **Survey Overview**: Shows the survey title and description.
- **Questions and Options**: Lists each question along with its possible options.
- **Short Answer Question Text**: Displays text for short-answer questions if applicable.
- **Related Page**: **MySurveyMain.razor (Entry Point)**
  - **Survey Title Input**: Allows users to search for a survey by its title.
  - **Go Button**: Navigates to the page showing the survey details.

### 5. ViewResponses(Main).razor

**Purpose**: Displays the responses collected for a specific survey.

**What it does**:

- **Survey Title**: Displays the title of the survey for which responses are shown.
- **Responses Table**: Lists all responses with columns for respondent name, submission date, and their responses to each question.
- **Related Page**: **ViewResponsesMain.razor**
  - **Survey List**: Displays a list of available surveys.
  - **Survey Links**: Each survey title links to its responses page.

### 6. Error.razor

**Purpose**: Displays a generic error page when something goes wrong in the application.

**What it does**:

- **Error Message**: A simple error message.
- **Request ID**: If available, shows the request ID to help debug issues.
- **Development Mode Tips**: Informs users about enabling the development environment for detailed error information.

## Overview of Connection Between Blazor App and WebAPI

The Blazor Web App (DNDBlazorApp) interacts with the WebAPI project primarily through the use of services (like FileService) and shared models.
Here's a step-by-step breakdown of how the Blazor app connects to the WebAPI:

### Services Used:

- **FileService**: Handles loading and saving surveys. It provides functionality to load the list of surveys, add new ones, and update existing surveys.
- Through dependency injection, the FileService is made available to Blazor components, enabling the management of survey data within the application.

### How It Works:

1. **Project Structure and References**
   In the Blazor app (DNDBlazorApp), the Program.cs file contains the necessary setup for connecting to the WebAPI.

```csharp
   <!-- Reference to the WebAPI project -->
   <ProjectReference Include="..\WebAPI\Survey.csproj" />
```

-The Blazor app references the WebAPI project in its .csproj file allowing the Blazor app to access services and models from the WebAPI project.
The connection is established between the two projects (Blazor app and WebAPI) by referencing the FileService in the Blazor app and using shared models like Survey, SurveyResponse, and Question.

2. **Service Registrations**
   In the Blazor app’s Program.cs, the FileService from the WebAPI project is registered with the dependency injection (DI) container

```csharp
// Register FileService in the DI container
builder.Services.AddSingleton<FileService>();
```

This ensures that the FileService is available to the Blazor components throughout the application.
By adding FileService as a singleton, the same instance of the service will be used across the app,
which is ideal for accessing and managing survey data in the file system (i.e., JSON files). 3. **Survey Data and Response Handling**
-The core of the interaction between the Blazor app and the WebAPI is the FileService. Here's how the service facilitates communication:

```csharp
@using WebAPI.Services
@inject FileService SurveyService
@inject FileService ResponseService
```

Through dependency injection, the FileService is made available to Blazor components, enabling the management of survey data within the application.

3.1. **Survey Data (Loading and Saving Surveys)**:

- The Blazor components interact with FileService to load and save surveys to JSON files on the server.

```csharp
  public List<Survey> LoadSurveys()
        {
            var jsonData = File.ReadAllText(_surveyFilePath);
            return JsonSerializer.Deserialize<List<Survey>>(jsonData) ?? new List<Survey>();
        }
```

- Loading Survey: When creating a survey or fetching available surveys, the Blazor app calls the LoadSurveys() method from FileService,
  which reads the surveys.json file and deserializes the data into a list of Survey objects.

```csharp
   public void SaveSurveys(List<Survey> surveys)
        {
            var jsonData = JsonSerializer.Serialize(surveys, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_surveyFilePath, jsonData);
        }
```

- Saving Survey: After a user creates a survey, the Blazor app creates a new Survey object and calls SaveSurveys() on FileService to save the survey data back into the surveys.json file.

  3.2. **Survey Data (Loading and Saving Responses)**:

- The Blazor app also interacts with responses via the FileService. This service provides methods for loading and saving survey responses to a file (responses.json).
  In FileService:

```csharp
      public List<SurveyResponse> LoadResponses()
       {
           var jsonData = File.ReadAllText(_responseFilePath);
           return JsonSerializer.Deserialize<List<SurveyResponse>>(jsonData) ?? new List<SurveyResponse>();
       }
```

In Blazor app (View Responses Page):

```csharp
     protected override async Task OnInitializedAsync()
   {
       // Load the survey based on the ID
       survey = SurveyService.LoadSurveys().FirstOrDefault(s => s.Id == id);

       if (survey != null)
       {
           // Load the responses for the specific survey ID
           var allResponses = ResponseService.LoadResponses();
           responses = allResponses.Where(r => r.SurveyId == id).ToList();
       }
   }
```

- Loading Responses: On the ViewResponses page, the Blazor app loads responses related to a specific survey by calling LoadResponses(). The responses are then displayed in a table format.
  In FileService:

```csharp
      public void SaveResponses(List<SurveyResponse> responses)
        {
            var jsonData = JsonSerializer.Serialize(responses, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_responseFilePath, jsonData);
        }
```

In Blazor app:

```csharp
    private async Task SubmitAnswers()
   {
       if (survey == null)
       {
           Console.WriteLine("No survey loaded.");
           return;
       }

       var newResponse = new SurveyResponse
       {
           SurveyId = survey.Id,
           RespondentName = respondentName,
           SubmittedAt = DateTime.UtcNow,
           Answers = userAnswers.Select(kvp => new ResponseAnswer
           {
               QuestionId = kvp.Key,
               SelectedOption = kvp.Value
           })
           .Concat(checkboxAnswers.SelectMany(kvp => kvp.Value.Select(option => new ResponseAnswer
           {
               QuestionId = kvp.Key,
               SelectedOption = option
           })))
           .ToList()
       };

       try
       {
           var responses = ResponseService.LoadResponses();
           responses.Add(newResponse);
           ResponseService.SaveResponses(responses);
           Console.WriteLine("Responses saved!");
           ResetForm();

           // Navigate back to the main survey page
           Navigation.NavigateTo("/AnswerSurvey");
       }
       catch (Exception ex)
       {
           Console.WriteLine($"Error saving responses: {ex.Message}");
       }
   }
```

- Saving Responses: When users submit answers to surveys, their responses are stored as SurveyResponse objects and saved back to responses.json through the SaveResponses() method.

```csharp

```

4. Models and Data Transfer
   - The models, such as Survey, SurveyResponse, Question, and Option, are defined in the WebAPI project under WebAPI.models.
     These models are shared between the Blazor app and the WebAPI, allowing both to use the same structure for data.

```csharp
 @using WebAPI.models
```

- These models are used for serializing/deserializing data between JSON files and the Blazor app.

5. **Data Flow**:
   - When the page loads, the services fetch survey data or responses from the backend, from a persistent store.
   - Data is dynamically rendered to the UI using Blazor’s data binding features (e.g., `@foreach` to display survey questions and responses).
   - After user interaction (such as submitting a survey response), data is sent back to the backend for storage.

## Key Requirements Implementation in the Web Application

This section outlines how the key requirements of our survey application were addressed through its design and implementation. The features discussed below showcase the user-friendly functionality that enables surveyors to create, distribute, and analyze surveys effectively:

### Survey Creation:

The application allows surveyors to create new surveys efficiently from the dashboard. Features like input fields for customizable titles and descriptions, the ability to add different question types (e.g., multiple choice, checkboxes, and short answer), and dynamic options management make the survey creation process seamless. For example, the "Add Question" and "Create" methods enable users to build and save surveys with persistent storage, while the form-reset functionality supports quick creation of subsequent surveys.

- As a Surveyor, I want to be able to create new surveys from the dashboard or home page, so that I can be efficient and fast.

```csharp
 <button @onclick="AddQuestion" class="acceptbtn">Add Question</button>
  private void AddQuestion()
    {
        questions.Add(new Question
        {
            Text = "",
            Type = selectedQuestionType,
            Options = selectedQuestionType == "ShortAnswer" ? null : new List<Option>()
        });
    }
```

- The "Add Question" functionality allows the surveyor to dynamically create survey questions directly in the interface

```csharp

 private async Task Create()
    {
        try
        {
            var surveys = SurveyService.LoadSurveys();
            int nextId = surveys.Any() ? surveys.Max(s => s.Id) + 1 : 1;

            var newSurvey = new Survey
            {
                Id = nextId,
                Title = title,
                Description = description,
                Questions = questions
            };

            surveys.Add(newSurvey);
            SurveyService.SaveSurveys(surveys);
            Console.WriteLine($"Survey Created: {newSurvey.Title}");
            ResetForm();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving survey: {ex.Message}");
        }
    }
```

- The Create method saves the new survey into the system with all entered details (title, description, questions) into persistent storage via SurveyService.
  The interface provides clear input areas for customization(Title, description,etc.-See below), supporting efficiency.

```csharp
 private void ResetForm()
{
    title = "";
    description = "";
    questions.Clear();
}
```

Resets the form to allow quick creation of subsequent surveys

- As a Surveyor, I want the surveys to have a customizable title and description, so that I can personalize the seurvey to my needs, requirements and topics.

```csharp
<label>Title</label>
                <input type="text" placeholder="Enter Survey Title" @bind="title" @bind:event="oninput" />

                <label>Description</label>
                <input type="text" placeholder="Enter Survey Description" @bind="description" @bind:event="oninput" />

```

- The @bind="title" and @bind="description" ensure that the values entered by the user are stored in the corresponding variables (title and description).
- In the Create method above, the entered title and description values are used to create a new Survey object thus allowing the user to customize their survey

```csharp

```

- As a Surveyor, I want to define different types of questions, so that I can diferentiate between quantitative or qualititative questions.

```csharp
 @if (question.Type == "MultipleChoice" || question.Type == "Checkboxes")
                    {
                        <div class="options">
                            @foreach (var option in question.Options)
                            {
                                <div>
                                    @if (question.Type == "MultipleChoice")
                                    {
                                        <input type="radio" disabled />
                                    }
                                    else if (question.Type == "Checkboxes")
                                    {
                                        <input type="checkbox" disabled />
                                    }
                                    <input type="text" placeholder="Enter Option"
                                           @bind="option.Text" @bind:event="oninput" />
```

- s

```csharp
 <button @onclick="() => AddOption(question)" class="add-option-btn">Add Option</button>
```

- Enables adding multiple options for specific question types.

### Data Analysis and Reporting:

Survey results are accessible via a dashboard that links to response pages for each survey. These pages provide a visual, table-based format displaying respondents’ names, submission dates, and their answers, ensuring that surveyors can easily analyze feedback.

By implementing these features, the application supports the end-to-end survey lifecycle, addressing key requirements for usability, efficiency, and accessibility.

- As a Surveyor, I want to view the results of each survey on my dashboard, so that I can quickly analyze the feedback in a visual format.

```csharp
 @foreach (var survey in surveys)
{
    <li>
        <a href="@($"/ViewResponses/{survey.Id}")">@survey.Title</a>
    </li>
}
```

- Each survey is rendered as a link. When clicked, it navigates to the /ViewResponses/{id} page for the corresponding survey.
- Provides an accessible dashboard-like interface to find and navigate to a survey's responses.

```csharp
 <table class="table">
    <thead>
        <tr>
            <th>Respondent Name</th>
            <th>Submission Date</th>
            <th>Responses</th>
        </tr>
    </thead>
    <tbody>
        @foreach (var response in responses)
        {
            <tr>
                <td>@response.RespondentName</td>
                <td>@response.SubmittedAt</td>
                <td>
                    <ul>
                        @foreach (var answer in response.Answers)
                        {
                            <li>@answer.QuestionId: @answer.SelectedOption</li>
                        }
                    </ul>
                </td>
            </tr>
        }
    </tbody>
</table>

```

- Displays the responses for a selected survey, showing the respondent’s name, submission date, and answers.
- Presents detailed feedback in an organized, visual table format.

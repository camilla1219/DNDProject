# Project Conclusion and Demonstration

This section provides a comprehensive summary of the project, broken into three key areas:

Final Development Update: This portion highlights the last-minute refinements made to improve the user experience and enhance the overall cohesiveness of the application. It also provides insight into specific technical adjustments and stylistic changes, such as modifications to the home page and the input fields.

Summary of Project Outcome: Here, we discuss the achievements of the project as a whole, emphasizing the development of a robust web survey platform. This summary details how the system successfully integrates backend operations with a user-friendly frontend to deliver core functionalities, including user registration, survey creation, and response collection.

Achieved Initial Requirements: This final section evaluates the project’s alignment with its original objectives. It categorizes the requirements into six major features, describing which were fully implemented, partially achieved, or left unaddressed due to time constraints.

By examining these sections, we provide a clear picture of the project's scope, accomplishments, and areas for potential future development.

Here you can look at the application in action: Check out our [Video Demonstration](https://youtu.be/C-qrX9fFd_A)

Unfortunately our Github repository wont recognize some of the images we have used for our login, logout and Access denied pages.

## Final Development Update

In order to finalize the project, small changes were made to enhance cohesiveness and presentation throughout the site as well as to enhance the user experience.
The display for the home page was finalised combining two visions from within our group and a name was chosen for the site to display.

```csharp
  <div class="col-12">
       <h2 class="fw-bolder">Welcome to <span class="text-danger">Feedback Fiesta</span></h2>
   </div>
```

Initially, there was text already input when a user would add an option 'New Option.' The user would then have to delete this before writing what they want.
This text was changed to automatically clear once the user starts typing to enhance the user experience. '

```csharp
  <!-- Clear the "New Option" text when user starts typing -->
       <input type="text" placeholder="Enter Option"
              @bind="option.Text"
              @bind:event="oninput"
              @onfocus="() => ClearOptionPlaceholder(option)" />

```

- When the user starts typing in the input field, the option.Text property is updated in real-time with the entered text.
- When the input field is focused (clicked/tapped), any placeholder or default text associated with the option is cleared by calling ClearOptionPlaceholder(option).
- css files were made for the View Survey, Survey Answer, My surveys, and Create Survey pages to make the site's presentation more cohesive

## Summary of Project Outcome

We have successfully developed a fully functioning web survey application by combining a robust backend with an interactive frontend. The Web API forms the brains of the operation of the backend, taking care of all the core operations that include data models, constructs, and various other logics. The frontend is developed using Blazor, providing the user interface through which users can interact with the application.

The application provides a fully functional login and registration system, securely integrated with an SQLite database. This allows for proper storage and management of user information.

Thanks to the feature allowing us to have a registration and login system, there are two different types of roles that we implemented: Users and Guests. Users are allowed to create and view their own surveys and analyze responses. Users can also choose question types to put in their surveys, allowing flexibility in how each survey can be applied. They can create a custom title and description for a survey and add different types of questions that suit their needs. The three types of questions available are multiple-choice, text-based, and rating scale questions, which provide great flexibility in the types of responses to be gathered. On the other hand, guests can participate in surveys but cannot create or track responses.

While not all planned features were fully implemented, all the core requirements of the application were successfully met. The survey platform is complete in terms of enabling users to register, create surveys, share them with respondents, and view collected responses. Some advanced features, like survey expiration, enhanced data visualization, and data export, were not realized within the project timeframe but remain feasible additions for future development.

All in all, the project produced a full-fledged survey platform that allows users flexibility in survey creation and reliable performance in gathering and analyzing responses.

## Achieved Initial Requirements for Online Survey Generator

### 1. User Registration and Login

- **Status**: Implemented
- **Details**:

  - `Login.razor` and `Register.razor` pages provide user authentication and registration.
  - Backend support for user credentials and roles exists through `UserAccount.cs` and `UserService.cs`.
  - Password reset functionality is not explicitly found.

### 2. Survey Creation

- **Status**: Implemented
- **Details**:

  - Users can create surveys with customizable titles, descriptions, and questions via `CreateSurvey.razor`.
  - Supported question types: multiple-choice, checkboxes, and short answers.
  - Surveys are saved and managed through `SurveyController.cs`.

### 3. Survey Distribution

- **Status**: Partially Implemented
- **Details**:

  - Surveys are retrievable via `SurveyController.cs` with unique IDs.
  - Explicit URL generation or email-sharing features are not implemented.

### 4. Survey Response Management

- **Status**: Implemented
- **Details**:

  - Respondents can answer surveys via `AnswerSurvey.razor` supporting all question types.
  - Responses are saved with timestamps and identifiers using `ResponseController.cs`.

### 5. Data Analysis and Reporting

- **Status**: Partially Implemented
- **Details**:

  - Responses are viewable via `ViewResponses.razor`, displaying respondent details and answers.
  - Visualization features (pie charts, bar graphs, etc.) are missing.
  - Data export functionality (CSV, Excel, PDF) is not implemented.

### 6. Survey Expiration and Closing

- **Status**: Not Fully Implemented
- **Details**:

  - No functionality for setting expiration dates or manually closing surveys is implemented.

## Summary of Achieved Requirements:

| Requirement                   | Status                |
| ----------------------------- | --------------------- |
| User Registration and Login   | Implemented           |
| Survey Creation               | Implemented           |
| Survey Distribution           | Partially Implemented |
| Survey Response Management    | Implemented           |
| Data Analysis and Reporting   | Partially Implemented |
| Survey Expiration and Closing | Not Fully Implemented |

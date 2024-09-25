# Project Formulation: Online Survey Generator

The objective of this project is to develop an online survey generator that enables users to easily create, distribute, and analyze customized surveys. Through a user-friendly interface, users can log into their accounts and design surveys with a variety of question formats, including multiple-choice, text responses, and rating scales. Once surveys are distributed, the system will automatically collect and analyze the responses, presenting the results in visually intuitive charts and graphs for more effective insights.

This tool is intended to provide a seamless experience for students, educators, and professionals who need to gather feedback, conduct research, or collect data. Similar to existing platforms like SurveyMonkey, this survey generator will be tailored for student use, focusing on simplicity, ease of use, and cost-effectiveness.

# Initial Requirements

### User Registration and Login:
* Users must be able to create accounts using username and password
* Passwords should be securely hashed and stored.
* Users must be able to reset their password via username.

### Survey Creation:
* As a user, I want to be able to create new surveys from the dashboard or home page, so that I can be efficient and fast.
* As a user, I want for surveys to have a customizable title, description, and cover image, so that I can personalize the seurvey to my needs, requirements and topics.
* As a user, I want to define different types of questions, so that I can diferentiate between quantitative or qualititative questions.
* As a user, I want to be allowed to set optional or required questions, so that I can allow certain questions to be skipped if they are optional or force certain questions to be answered if they are obligatory or needed.

### Survey Distribution:
* As a user I would like to share my surveys via a unique URL so that I can easily share my surveys and gather more answers.
* As a user I would like to share my surveys through email invitations so that I can share my surveys with my colleagues.
  
### Survey Response Management:
* Responses must be stored in the database, tagged by survey ID and user ID (if logged in)
* Support for both anonymous and identified (logged-in) responses.
* Ensure that users cannot submit the same survey more than once (unless specified by the survey creator).

### Data Analysis and Reporting:
* Users should be able to view the results of each survey through their dashboard.
* Pie charts, bar graphs, and line charts.
* Allow users to export results as CSV, Excel, or PDF files.

### Survey Expiration and Closing:
* As a user I would like to be able to set an expiration date/manually close my survey when I no longer need it, so that I can close my survey when I’ve gathered the desired number of answers.
* As a user I would like the surveys to not accept new answers after I’ve closed them to not skew the results after I’ve run them.

### Objective:
* Develop a web-based platform for creating, managing, and distributing surveys. The platform will enable users to design surveys, view analytics, and share results, all through RESTful APIs and web services.

### Technologies:
* C# for backend services
* RESTful API for interaction between frontend and backend
* Blazer for web application
* Database for storing survey data and responses (e.g., MySQL, PostgreSQL)


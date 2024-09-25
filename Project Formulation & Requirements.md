# Project Formulation: Online Survey Generator

The objective of this project is to develop an online survey generator that enables users to easily create, distribute, and analyze customized surveys. Through a user-friendly interface, users can log into their accounts and design surveys with a variety of question formats, including multiple-choice, text responses, and rating scales. Once surveys are distributed, the system will automatically collect and analyze the responses, presenting the results in visually intuitive charts and graphs for more effective insights.

This tool is intended to provide a seamless experience for students, educators, and professionals who need to gather feedback, conduct research, or collect data. Similar to existing platforms like SurveyMonkey, this survey generator will be tailored for student use, focusing on simplicity, ease of use, and cost-effectiveness.

### Objective:
* Develop a web-based platform for creating, managing, and distributing surveys. The platform will enable users to design surveys, view analytics, and share results, all through RESTful APIs and web services.

### Technologies:
* C# for backend services
* RESTful API for interaction between frontend and backend
* Blazer for web application
* Database for storing survey data and responses (e.g., MySQL, PostgreSQL)

## Why we chose this project:

We chose this project because it presents new challenges that push our technical capabilities while remaining manageable in terms of scope and difficulty. We've previously worked with database integration and connecting user interfaces to backend systems, but this project adds an exciting new layer of complexity with the introduction of real-time data visualization for survey results. We also aimed to develop something practical that we, as students, could benefit from—whether for gathering feedback, conducting research, or collaborating on academic projects. This blend of familiar and new challenges makes the survey generator both a practical and engaging endeavor.

# Initial Requirements

### User Registration and Login:
* As a user, I want to be able to create an account using a username and password, so that I can securely log into the platform and access my surveys.
* As a user, I want to reset my password if I forget it, so that I can regain access to my account via my username.

### Survey Creation:
* As a user, I want to be able to create new surveys from the dashboard or home page, so that I can be efficient and fast.
* As a user, I want for surveys to have a customizable title, description, and cover image, so that I can personalize the seurvey to my needs, requirements and topics.
* As a user, I want to define different types of questions, so that I can diferentiate between quantitative or qualititative questions.
* As a user, I want to be allowed to set optional or required questions, so that I can allow certain questions to be skipped if they are optional or force certain questions to be answered if they are obligatory or needed.

### Survey Distribution:
* As a user I would like to share my surveys via a unique URL so that I can easily share my surveys and gather more answers.
* As a user I would like to share my surveys through email invitations so that I can share my surveys with my colleagues.
  
### Survey Response Management:
* As a user, I want to have my responses saved in a database along with my user ID and the survey ID (if I’m logged in), so that I can submit my answers securely and retrieve them later if needed, while associating them with the correct survey.
* As a user, I want to be able to submit responses either anonymously or while logged in, so that I can choose to either associate my responses with my user account or submit them privately.
* As a user, I want to be prevented from submitting the same survey multiple times, unless the survey creator allows multiple submissions, so that I can only submit valid responses and avoid duplicate entries.

### Data Analysis and Reporting:
* As a user, I want to view the results of each survey on my dashboard, so that I can quickly analyze the feedback in a visual format.
* As a user, I want to see the survey results displayed in pie charts, bar graphs, and line charts, so that I can easily interpret the data.
* As a user, I want to export survey results as CSV, Excel, or PDF files, so that I can further analyze or share the data with others.

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


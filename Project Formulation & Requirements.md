# Project Formulation: Online Survey Generator

The objective of this project is to develop an online survey generator that enables users to easily create, distribute, and analyze customized surveys. Through a user-friendly interface, users can log into their accounts and design surveys with a variety of question formats, including multiple-choice, text responses, and rating scales. Once surveys are distributed, the system will automatically collect and analyze the responses, presenting the results in visually intuitive charts and graphs for more effective insights.

This tool is intended to provide a seamless experience for students, educators, and professionals who need to gather feedback, conduct research, or collect data. Similar to existing platforms like SurveyMonkey, this survey generator will be tailored for student use, focusing on simplicity, ease of use, and cost-effectiveness.

## User Registration and Login:
* Users must be able to create accounts using username and password
* Passwords should be securely hashed and stored.
* Users must be able to reset their password via username.

## Survey Creation:
* Users should be able to create new surveys from their dashboard.
* Surveys should have a customizable title, description, and cover image .
* Users can define different types of questions:
* Allow users to set optional or required questions.

## User Registration and Login:
* Allow users to share surveys via a unique URL.
* Provide email invitations with an embedded survey link.

## Survey Response Management:
* Responses must be stored in the database, tagged by survey ID and user ID (if logged in)
* Support for both anonymous and identified (logged-in) responses.
* Ensure that users cannot submit the same survey more than once (unless specified by the survey creator).

## Data Analysis and Reporting:
* Users should be able to view the results of each survey through their dashboard.
* Pie charts, bar graphs, and line charts.
* Allow users to export results as CSV, Excel, or PDF files.

## Survey Expiration and Closing:
* Users must be able to set an expiration date for a survey or manually close it.
* After expiration, the survey should not accept new responses.

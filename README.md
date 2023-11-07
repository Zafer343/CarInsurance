## C# Projects
There are some small projects I made using C# and .NET Framework
# CarInsurance
This project is an MVC web application that mimics a car insurance website. It takes user's input on policy information and calculates a quote based on business logic. There is also an admin page that allows staff to view all of the quotes that have been issued.

# Car Insurance Quote Application - Implementation Details

The Car Insurance Quote application has been developed to facilitate users in getting insurance quotes through an online form and allows administrators to view submitted quotes. Below are the implementation details for the core components of the application.

## HomeController - Form Submission and Quote Calculation

`HomeController` manages the primary interface for users to submit their insurance details and receive a quote.

### Key Features

- **Index View**: Displays the form for users to enter their insurance details.
- **SendForm Method**: Processes the submitted form data to calculate the insurance quote.

### SendForm Method

When the user submits the form, the `SendForm` method is invoked with the form data as parameters:

- The method validates the input data to ensure that all required fields are provided.
- It calculates the insurance quote based on several factors, including:
  - The age of the insured.
  - The year of the car.
  - The make and model of the car.
  - The number of speeding tickets.
  - Whether the insured has had a DUI.
  - The type of coverage selected (Full or Basic).

#### Calculation Logic

- The base quote starts at a predefined amount, and adjustments are made based on the user's details.
- Age, car year, and car make and model are significant factors that increase the base amount.
- Having a DUI or speeding tickets will also increase the quote.
- The final quote is adjusted based on the type of coverage selected.

After calculating the quote, it's saved to the database, and the quote amount is displayed to the user.

## AdminController - Quote Review

`AdminController` is intended for administrative users to review and manage the submitted quotes.

### Key Features

- **Index Method**: Retrieves and displays a list of all insurance quotes that have been submitted.

#### Index Method

- The method queries the database for all stored insurance quotes.
- It maps the data retrieved from the database to `QuoteVm` (View Model) for presentation.
- A list of `QuoteVm` instances is passed to the view to display the insurance quotes.

## Database Model - InsuranceQuote

The `InsuranceQuote` model represents the data structure for storing insurance quotes with the following details:

- First and Last Name of the insured.
- Email Address.
- Date of Birth.
- Car Year, Make, and Model.
- DUI flag.
- Number of Speeding Tickets.
- Type of Coverage.
- Calculated Quote Amount.

The model corresponds to a database table where the insurance quotes are persisted after being submitted by users.

## Views

There are separate views for user form submission and for admin review:

- **Home/Index.cshtml**: The form for users to request an insurance quote.
- **Admin/Index.cshtml**: The view for administrators to see all the quotes.

The application leverages the MVC pattern to separate concerns, improve maintainability, and provide a clear structure for future development and enhancements.


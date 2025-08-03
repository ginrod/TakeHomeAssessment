## Project Overview and Development Notes

Before starting the task, I conducted a quick analysis of the base repository to understand the structure and practices in place.

I noticed that the project is built with **ASP.NET Core**, following solid architectural conventions. It features a clear separation of concerns between the `Application/Domain`, `Infrastructure`, and `Web API` layers. The client component is also separated, which is a good practice.

For the **client**, I decided to use **Razor Pages** with **vanilla JavaScript**.

While reviewing the base project, I identified some **inconsistencies in naming conventions** — some identifiers used PascalCase, others camelCase; some classes or variables were singular while others were plural. I aimed to maintain consistency moving forward.

---

## Implementation Details and Assumptions

To begin, I defined a repository (mostly empty, as the inherited functions from `IEntitiesRepository` were sufficient), and implemented an `OfficesController` with a basic `GetAll` endpoint. This became the entry point of the app.

Each contact is required to belong to at least one office. The first view allows the user to select an office, and based on that context, perform a **search for contacts by name**.

The **Offices view** displays a dropdown with all available offices. For simplicity, I didn’t implement pagination here, but in a real scenario — with dozens, hundreds, or thousands of offices — pagination and search functionality would be essential.

Upon selecting an office, the app redirects to the **Contacts page**. There, I completed the **search by term** feature, and added a **"Find All"** option to retrieve all contacts within the selected office. This view also includes **pagination**, with a fixed page size of 5 records. Contact details are displayed in a table.

---

## Sample Data & Test Preparation

The original sample data included two contacts (Mark and Alejandro) and one office (Default Office). Mark had no associated office, while Alejandro was linked to the Default Office. Based on my rule that **every contact must belong to at least one office**, I associated Mark with the Default Office.

I also created:

- **5 dummy offices**, and
    
- 2 themed for testing: `Granite` and `Continuum`.
    

Then I added **20 sample contacts**:

- 10 with emails under `@wearecontinuum.com`
    
- 10 under `@granite.ie`
    

Since Continuum is part of Granite Digital, **all 20 contacts are linked to the Granite office**, while only the `@wearecontinuum.com` contacts are additionally associated with Continuum.

All sample data is added **in-memory**, similar to the original seed logic.

---

## Optional Features (If Given More Time)

If this were a production-ready app or I had 3–4 more days, I would have implemented:

- **Authentication & Authorization** using **Azure AD B2C**.
    
    - I would configure **email-based sign-up** and **sign-in**, with **role-based access**:
        
        - **Admins** could view and select from all offices.
            
        - **Regular users** would be restricted to searches within their own offices.

If more time were available, I would also focus on improving **code quality and maintainability** through the following practices:

### Unit and Integration Testing

I would implement a testing layer for both the API and client components using:

- **xUnit** as the main testing framework for the API, due to its popularity, extensibility, and support in ASP.NET Core projects.
    
- **FluentAssertions** for readable and expressive assertions.
    
- **Moq** for mocking dependencies and services.
### Error Handling and UI Feedback

The current implementation includes **basic error handling in the UI**, mostly with inline messages. With more time, I would:

- Introduce a **notification library** such as **Toastr.js** or **SweetAlert2** for better visual feedback on success/error scenarios.
    
- Handle edge cases more robustly in both the API (via middleware) and the UI.

### Continuous Integration and Deployment

To streamline development and ensure reliability:

- I would write **CI/CD scripts** (GitHub Actions) to:
    
    - Automatically build and test the solution on every pull request.
        
    - Deploy both the API and Client to **Azure App Services** after successful tests on the `main` branch.
        

This would help ensure changes to either the backend or frontend are **safely deployed and verified** in a consistent manner.

Additionally, I would enhance **observability** by incorporating more comprehensive logging across both layers, capturing key actions, edge cases, and error states to aid in future debugging and monitoring.

---

## Running the App Locally

- Use **Visual Studio 2022**.
    
- Set the **Client project as startup**.
    
- In a separate terminal, navigate to the `ContactSystem` folder and run the API using `dotnet run`.
    

> When using Visual Studio, Razor `.cshtml` and JS changes can be seen via **Hot Reload** without recompilation.

You may invert the startup order depending on what you want to test (API or UI).

---

## Demo & Deployment

A walkthrough demo is available for both:

- General users exploring the app. See the video (comming soon*)
    
- Developers reviewing the code. See the video (comming soon*)

You can test the deployed version of the app [here](https://contactsystemtakehomeassessmentclient-gqgxdrerayfxgfd0.northeurope-01.azurewebsites.net/) 
# Bearing Management UI

## Overview
Bearing Management UI is a Blazor Web Assembly Standalone frontend application for managing bearings data. 
It interacts with the Bearing Management API and provides a user-friendly interface for authentication, viewing, adding, updating, and deleting bearings.

## Prerequisites

Ensure you have the following installed:

- .NET SDK 9.0
- Visual Studio 2022 or higher / VS Code with C# extension / Rider

## Getting Started

### Clone the Repository

```sh
git clone https://github.com/janegarciu/BearingManagementSystemUI.git
cd BearingManagementUI
```

## Connecting to the API
The UI communicates with the Bearing Management API. Ensure the API is running before launching the UI.

- **Default API URL:** `https://localhost:5001`
- If using a different API URL, update the configuration in `appsettings.json` or environment variables. 
- Under wwroot folder find `appsettings.json` file and modify it to match api url:
   ```sh
   {
   "ApiUrl": "https://localhost:5001/"
   }
   ```
   
### Build and Run the Project

To restore dependencies and build the project:

```sh
dotnet restore
dotnet build
```

To run the API locally:

```sh
dotnet run --urls "https://localhost:7295;http://localhost:5177"
```

The UI will be available at `https://localhost:7295` (or the configured port).


### Running with IDE
Running with IDE is also possible by opening .sln file in IDE and selecting
running profile from the section above.

### Accessing deployed to Azure app:
You can also access the project through this url:
https://test-project-web-app-1.azurewebsites.net/bearings


### Functionalities on the page

## User Guide
### Authentication and Access
To access the Bearings page, follow these steps:

1. **Register a User:**
   - Navigate to the **Register** page.
   - Enter your **login** and **password**.
   - Click **Register** to create an account.

2. **Login to Your Account:**
   - Navigate to the **Login** page.
   - Enter your **login** and **password**.
   - Click **Login** to access the system.

3. **Navigating the Bearings Page:**
   - Once logged in, you will be redirected to the **Bearings Table**.
   - Here, you can **view**, **edit**, **delete**, and **create** new bearings.
# Blazor Authentication Implementation

This document outlines the authentication and authorization features implemented in the Blazor web application, more specifically, in **DNDBlazorApp**.

## Overview

The application uses **Blazor Server** with cookie-based authentication to manage user access and roles effectively. Here's a detailed breakdown of the authentication implementation.

## Key Features

1. **User Authentication**

   - Cookie-based authentication is implemented using `CookieAuthenticationDefaults.AuthenticationScheme`.
   - Login functionality is designed to verify user credentials and assign roles.

2. **Role-based Authorization**

   - Access to pages and components is restricted based on user roles.
   - Unauthorized access redirects users to the `AccessDenied` page.

3. **Logout Functionality**

   - Users can log out via a designated logout button.
   - Logout invalidates the authentication cookie and refreshes the page.

## Implementation Details

### 1. **Configuration in `Program.cs`**

The `Program.cs` file configures services for authentication and authorization. Below are the key configurations:

- **Authentication Scheme:** Cookie-based authentication using `CookieAuthenticationDefaults.AuthenticationScheme`.
- **Login Path:** Specified URL for the login page.
- **Access Denied Path:** URL for the access denied page.
- **Database Integration:** `AppDbContext` is configured for user account management.

### Code Snippet:

```csharp
using DNDBlazorApp.Components;
using DNDBlazorApp.Data;
using WebAPI.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth_token";
        options.LoginPath = "/login";
    });
```

- Authentication services are added with:

```csharp
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });
```

- Authorization services are added:

```csharp
builder.Services.AddAuthorization();
```

### 2. **Database Integration Database Context `AppDbContext.cs`**

Manages database interactions for retrieving user accounts.

- User details, including roles, are fetched from a SQL Server database.
- The `UserAccount` entity maps user credentials and roles.

### Code Snippet:

```csharp
﻿using DNDBlazorApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace DNDBlazorApp.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {

    }

    public DbSet<UserAccount> UserAccounts { get; set; }
}
```

### 3. Razor Components

#### 3.1. **Login Page (`/Account/Login.razor`)**

This page allows users to input their credentials and authenticate themselves.

- Implements a login form using `EditForm` and `LoginViewModel` for data binding.
- Validates user credentials against the database.
- Creates authentication cookies for valid users.

#### Code Snippet:

```razor
﻿@page "/login"
@using System.Security.Claims
@using DNDBlazorApp.Data
@using DNDBlazorApp.Models.ViewModels
@using Microsoft.AspNetCore.Authentication
@using Microsoft.AspNetCore.Authentication.Cookies
@inject NavigationManager navigationManager
@inject AppDbContext appDbContext

<div class="row">
    <div class="col-lg-4 offset-lg-4 pt-4 pb-4 border">
        <EditForm Model="@Model" OnValidSubmit="Authenticate" FormName="LoginForm">
            <DataAnnotationsValidator />
            <div cl
```

#### 3.2. **Access Denied Page (`/Account/AccessDenied.razor`)**

Displays a message when a user attempts to access unauthorized content.

- Displays a friendly message for unauthorized access attempts.

#### Code Snippet:

```razor
﻿@page "/accessDenied"

<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-body flex-column">
                <div class="text-center">
                    <img src="/images/accessDenied.png" style="max-height:5rem" />
                </div>
                <div class="text-center mt-2">
                    <span class="text-danger fw-bolder">You don't have permission to access this page.</span>
                </div>
            </div>
        </d
```

#### 3.3. **Logout Page (`/Account/Logout`)**

Handles user logout and redirects them.

- Signs out the user using `SignOutAsync`.
- Automatically refreshes the page.

#### Code Snippet:

```razor
﻿@page "/logout"
@using Microsoft.AspNetCore.Authentication
@inject NavigationManager navigationManager

<div class="row">
    <div class="col-12">
        <div class="card">
            <div class="card-body flex-column">
                <div class="text-center">
                    <img src="/images/logout.png" style="max-height:5rem" />
                </div>
                <div class="text-center mt-2">
                    <span class="text-danger fw-bolder">You've successfully logged out o
```

#### 3.4. **Authorization in Components**

- Components use the `[Authorize]` attribute to enforce access control.
- Example:

  ```csharp
  [Authorize(Roles = "Administrator")]
  public partial class WeatherComponent : ComponentBase { }
  ```

#### 3.5. **AuthorizeView Usage**

- Conditionally renders UI elements based on the user's authentication status or roles.
- Example:

  ```razor
  <AuthorizeView>
      <Authorized>
          <button @onclick="Logout">Logout</button>
      </Authorized>
      <NotAuthorized>
          <a href="/Account/Login">Login</a>
      </NotAuthorized>
  </AuthorizeView>
  ```

#### 3.6. **Route Guarding**

- Routes are guarded using `AuthorizeRouteView` in `App.razor`.

  ```razor
  <Router AppAssembly="@typeof(App).Assembly">
      <AuthorizeRouteView DefaultLayout="@typeof(MainLayout)" />
      <NotFound>
          <CascadingAuthenticationState>
              <LayoutView Layout="@typeof(MainLayout)">
                  <p>Sorry, there's nothing at this address.</p>
              </LayoutView>
          </CascadingAuthenticationState>
      </NotFound>
  </Router>
  ```

### 4. View Model `LoginViewModel.cs`

Defines the properties for Email and password with validation.

### Code Snippet:

```csharp
﻿using System.ComponentModel.DataAnnotations;

namespace DNDBlazorApp.Models.ViewModels;

public class LoginViewModel
{
    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter User Name")]
    public string? Email { get; set; }

    [Required(AllowEmptyStrings = false, ErrorMessage = "Please enter Password")]
    public string? Password { get; set; }
}
```

### 5. Layout and Navigation

The `MainLayout.razor` and `NavMenu.razor` components include role-based UI visibility.

### Code Snippet:

```razor
﻿@inherits LayoutComponentBase

<div class="page">
    <div class="sidebar">
        <NavMenu />
    </div>

    <main>
        <div class="top-row px-4">
            <a href="https://github.com/camilla1219/DNDProject" target="_blank">About</a>
            <AuthorizeView>
                <Authorized>
                    <a href="/logout">Logout</a>
                </Authorized>
                <NotAuthorized>
                    <a href="/login">Login</a>
                </NotAuthorized>

```

```razor
﻿<div class="top-row ps-3 navbar navbar-dark">
    <div class="container-fluid">
        <a class="navbar-brand" href="">Simple Survey</a>
    </div>
</div>

<input type="checkbox" title="Navigation menu" class="navbar-toggler" />

<div class="nav-scrollable" onclick="document.querySelector('.navbar-toggler').click()">
    <nav class="flex-column">
        <div class="nav-item px-3">
            <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                <span class="bi bi-house-
```

## Testing

1. **Login**:

   - Navigate to `/Account/Login`.
   - Test with valid and invalid credentials.
   - Verify proper role-based navigation.

2. **Role Restrictions**:

   - Check restricted pages by manually entering URLs.
   - Verify access denial and redirection behavior.

3. **Logout**:

   - Test logout functionality and automatic redirection.

4. **Cookie Management**:

   - Inspect authentication cookies for correct behavior.

## Security Notes

- Unauthorized users are restricted at both route and component levels.
- Cookies are configured with appropriate expiration times.

## Conclusion

This document summarizes the implementation of authentication and authorization in the Blazor web application. For further details, refer to the codebase.

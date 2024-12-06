# Blazor Data Access in the DNDProject

## Overview

The **DNDProject** utilizes Blazor for its front-end and implements data access through Entity Framework Core for database operations. SQLite is the database provider, and data is managed using a combination of `DbContext`, services, and controllers. Some components also handle data in-memory and use JSON files for persistence.

---

## ORM Implementation

### Entity Framework Core

The project integrates **Entity Framework Core** for database operations. Key files and their roles:

### 1. `AppDbContext.cs`

This is the central class for interacting with the database. It inherits from `DbContext` and manages the `UserAccount` entity.

```csharp
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) { }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }
}
```

- **`DbSet<UserAccount>`**: Represents the `user_account` table in the database.

### 2. `UserAccount.cs`

Defines the `UserAccount` entity with mappings to the database table and columns.

```csharp
[Table("user_account")]
public class UserAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("id")]
    public int Id { get; set; }

    [Column("email")]
    [MaxLength(100)]
    public string? Email { get; set; }

    [Column("password")]
    [MaxLength(100)]
    public string? Password { get; set; }

    [Column("role")]
    [MaxLength(20)]
    public string? Role { get; set; }
}
```

- **Attributes**:

  - `[Key]` defines the primary key.
  - `[Table]` and `[Column]` map the entity to the database schema.
  - `[DatabaseGenerated]` configures auto-incrementing fields.

### 3. `Program.cs`

Registers `AppDbContext` in the dependency injection container and configures SQLite as the database provider.

```csharp
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DbConnection")));
```

- **Database Provider**: SQLite.
- **Connection String**: Defined in `appsettings.json` as:

  ```json
  "ConnectionStrings": {
    "DbConnection": "Data Source=Database/blazor_app.db"
  }
  ```

---

## Benefits of Using ORM in the Project

### 1. Simplified Data Representation

Entity Framework Core allows representing database tables as classes, aligning the database schema with the application's object-oriented design. For example, the `UserAccount` entity maps directly to the `user_account` table.

### 2. Automated CRUD Operations

ORMs eliminate the need to write manual SQL queries for basic operations like creating, reading, updating, and deleting records. For example, `context.UserAccounts.Add(user)` in Entity Framework simplifies data insertion.

### 3. Secure Querying

Entity Framework automatically parameterizes queries, mitigating the risk of SQL injection. This is crucial when handling sensitive data, such as passwords, as seen in the `UserService.cs`.

### 4. Schema Management with Migrations

Entity Framework migrations simplify schema changes, such as adding new columns (e.g., a `LastLogin` timestamp), by generating SQL scripts automatically. This reduces errors compared to manually writing SQL.

### 5. Testing and Mocking

The `AppDbContext` class can be mocked, enabling isolated testing of authentication and other logic without a physical database.

---

## LINQ Usage in the Project

The project extensively uses LINQ for querying both the database and in-memory collections. Key LINQ features include:

### 1. Database Queries

#### Example in `UserService.cs`:

```csharp
public UserAccount? Verify(string email, string password)
{
    return context.UserAccounts.FirstOrDefault(x => x.Email.ToLower() == email.ToLower()
            && x.Password == password);
}
```

- **Explanation**: `FirstOrDefault` retrieves the first matching record or `null`.

#### Adding a User:

```csharp
bool isExist = context.UserAccounts.Any(x => x.Email == user.Email);
if (!isExist)
{
    context.UserAccounts.Add(user);
    context.SaveChanges();
}
```

- **Explanation**: `Any` checks if a record exists, and `Add` inserts a new record.

### 2. In-Memory Collection Queries

#### Example in `ResponseController.cs`:

```csharp
var response = _responses.FirstOrDefault(r => r.Id == id);
if (response != null) _responses.Remove(response);
```

- **Explanation**: Queries in-memory collections to retrieve and manipulate items.

---

## Advantages of LINQ over Traditional SQL

### 1. Language Integration

LINQ queries are strongly typed and checked at compile time, reducing runtime errors. Example:

```csharp
var users = context.UserAccounts.Where(u => u.Role == "Admin").ToList();
```

### 2. Readability and Maintainability

LINQ syntax is consistent with C# constructs, improving code readability.

### 3. Security

LINQ inherently protects against SQL injection by parameterizing queries.

### 4. Data Source Flexibility

LINQ can query a variety of data sources, such as databases, XML, and JSON. For example:

```csharp
var surveyIds = _surveys.Select(s => s.Id).ToList();
```

---

## Services

### `UserService.cs`

Handles business logic and database operations for the `UserAccount` entity.

#### Key Methods

1. **Save User**:

   Adds a new user to the database if they do not already exist.

   ```csharp
   public bool SaveUser(UserAccount user)
   {
       bool isExist = context.UserAccounts.Any(x => x.Email == user.Email);
       if (!isExist)
       {
           context.UserAccounts.Add(user);
           context.SaveChanges();
           return true;
       }
       return false;
   }
   ```

2. **Verify User**:

   Checks for a matching user based on email and password.

   ```csharp
   public UserAccount? Verify(string email, string password)
   {
       return context.UserAccounts.FirstOrDefault(x => x.Email.ToLower() == email.ToLower() && x.Password == password);
   }
   ```

---

## Non-ORM Data Access

Some components manage data outside of a database using JSON files and in-memory collections.

### `ResponseController.cs` and `SurveyController.cs`

- Operate on data stored in JSON files via a `FileService`.
- Examples of in-memory operations:

```csharp
var response = _responses.FirstOrDefault(r => r.Id == id);
if (response != null) _responses.Remove(response);
_fileService.SaveResponses(_responses);
```

```csharp
var newResponse = new SurveyResponse { Id = _responses.Max(r => r.Id) + 1 };
_responses.Add(newResponse);
_fileService.SaveResponses(_responses);
```

---

## Summary

- **ORM Usage**:

  - Entity Framework Core is used for database operations with `AppDbContext` and the `UserAccount` entity.
  - SQLite serves as the database provider.
  - Key benefits include automated CRUD operations, secure querying, and schema migrations.

  - **LINQ**:

  - LINQ is leveraged for both database and in-memory data manipulation.
  - Provides strong typing, compile-time checks, and SQL injection protection.

- **Non-ORM Usage**:

  - JSON-based file storage is used for survey and response data.

This design allows the project to leverage the power of Entity Framework for structured data while also maintaining flexibility with in-memory and file-based storage for other components.

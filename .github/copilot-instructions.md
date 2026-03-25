# MermaidSharp

MermaidSharp is a comprehensive .NET wrapper library for creating
Mermaid flowcharts with full syntax support. The solution includes a
core .NET 8 library, unit tests, and sample applications (MVC Web and
Blazor) demonstrating usage.

Always reference these instructions first and fallback to search or bash
commands only when you encounter unexpected information that does not
match the info here.

---

## Coding Standards

### General Principles

-   **Target Framework:** All projects target .NET 8.0. Ensure
    compatibility with this version.
-   **Language:** Use C# for all code unless otherwise specified.
-   **Readability:** Write clear, self-explanatory code. Use meaningful
    variable, method, and class names.
-   **Consistency:** Follow consistent naming conventions and code
    formatting throughout the solution.
-   **Comments:** Add inline comments only where necessary to clarify
    complex logic and in english, always in english.
-   **Documentation Comments:** All public types and members must
    include XML documentation comments following the conventions below.
-   **Error Handling:** Use structured exception handling. Avoid
    swallowing exceptions; log or rethrow as appropriate.
-   **SOLID Principles:** Adhere to SOLID design principles for
    maintainable and extensible code.
-   **Dependency Injection:** Prefer constructor injection for
    dependencies.
-   **Async/Await:** Use asynchronous programming patterns where
    appropriate, especially for I/O-bound operations.
-   **Magic Numbers:** Avoid magic numbers; use named constants or
    enums.
-   **File Organization:** One class per file. Organize files into
    appropriate folders by feature or layer.

### Naming Conventions

-   **Classes & Interfaces:** PascalCase (e.g., `UserService`,
    `IUserRepository`)
-   **Methods & Properties:** PascalCase (e.g., `GetUserById`)
-   **Private & Public variables:** camelCase (e.g., `_userId`,
    `UserId`)
-   **Parameters:** camelCase (e.g., `userId`)
-   **Constants:** PascalCase (e.g., `DefaultTimeout`)
-   **Unit Test Methods:** Use descriptive names indicating the scenario
    and expected outcome (e.g.,
    `GetUserById_ReturnsUser_WhenUserExists`)

### Code Style

-   **Braces:** Use Allman style (braces on new lines).
-   **Indentation:** Use one tabulation per indentation level.
-   **Line Length:** Limit lines to 120 characters.
-   **Usings:** Place `using` statements outside the namespace and
    remove unused usings.

---

## Documentation

These rules apply to all XML documentation comments.

### General Rules

-   Use XML documentation comments with `///`
-   Write in **English**
-   Use **third person**
-   Start with an **action verb**
-   Describe **behavior**, not implementation details
-   End sentences with a period
-   Keep `<summary>` short (1--2 lines)
-   Do not repeat the member name
-   Avoid vague descriptions

### Standard Structure

``` csharp
/// <summary>
/// Brief description of what the member does.
/// </summary>
```

### Methods

Always include: - `<summary>` - `<param>` for each parameter -
`<returns>` if the method returns a value - `<exception>` for each
thrown exception

``` csharp
/// <summary>
/// Calculates the total price including tax.
/// </summary>
/// <param name="price">Base price before tax.</param>
/// <param name="taxRate">Tax rate as a decimal (e.g., 0.20 for 20%).</param>
/// <returns>Total price including tax.</returns>
/// <exception cref="ArgumentOutOfRangeException">
/// Thrown when price or taxRate is negative.
/// </exception>
public decimal CalculateTotal(decimal price, decimal taxRate)
```

### Properties

Describe the exposed value.

- Read-only:

``` csharp
/// <summary>
/// Gets the current user name.
/// </summary>
public string UserName { get; }
```

- Read/write:

``` csharp
/// <summary>
/// Gets or sets the timeout duration in seconds.
/// </summary>
public int Timeout { get; set; }
```

### Classes / Structs

- Describe what the type represents.

``` csharp
/// <summary>
/// Represents a customer order in the system.
/// </summary>
public class Order
```

### Interfaces

- Describe the contract and responsibility.

``` csharp
/// <summary>
/// Defines persistence operations for entities.
/// </summary>
public interface IRepository<T>
```

### Useful Tags

  Tag                               |Purpose
  ----------------------------------|--------------------------
  `<summary>`                       |Main short description
  `<param>`                         |Parameter description
  `<returns>`                       |Return value description
  `<remarks>`                       |Additional details
  `<example>`                       |Usage example
  `<exception>`                     |Thrown exceptions
  `<see cref="TypeOrMember"/>`      |Internal reference
  `<seealso cref="TypeOrMember"/>`  |Related reference

### Full Template

- Always include a `<summary>` and relevant tags for methods, properties, classes, and interfaces.

``` csharp
/// <summary>
/// Creates a new order for the specified customer.
/// </summary>
/// <param name="customerId">Unique customer identifier.</param>
/// <param name="items">List of ordered items.</param>
/// <returns>The created order instance.</returns>
/// <exception cref="ArgumentNullException">
/// Thrown when items is null.
/// </exception>
/// <remarks>
/// The order is persisted immediately after creation.
/// </remarks>
public Order CreateOrder(Guid customerId, IEnumerable<Item> items)
```

---

## Copilot Usage

-   **Adhere to these standards** when generating or modifying code.
-   **Prefer existing patterns** and conventions found in the solution.
-   **Generate code that is ready to use** and fits seamlessly into the
    current structure.
-   **Ensure XML documentation is present** for all public members.
-   **Document any deviations** from these standards in pull requests or
    code reviews.

---

## Unit Test Standards

### General Principles

-   **Test Framework:** Use MSTest as appropriate for the project.
-   **Test Naming:** Use descriptive method names:
    `MethodName_ExpectedBehavior_StateUnderTest`.
-   **Test Structure:** Follow Arrange-Act-Assert (AAA) pattern in all
    tests.
-   **Comments:** Add inline comments only where necessary to clarify
    complex logic and in english and for Arrange-Act-Assert sections,
    always in english.
-   **Isolation:** Each test should be independent and not rely on the
    outcome of other tests.
-   **Mocking:** Use mocks or fakes for external dependencies
    (e.g. APIs) to ensure tests are deterministic.

---

## Working Effectively

### Prerequisites

Install the required .NET SDKs in this exact order:
- `.NET Framework 4.8 SDK` - Required for core library and tests
- `.NET 8.0 SDK` - Required for core library and tests
- `.NET 9.0 SDK` - Required for sample applications

``` bash
# Install .NET Framework 4.8 (required for tests)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 4.8

# Install .NET 8 (required for tests)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0

# Install .NET 9 (for sample applications)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 9.0

export PATH="/home/runner/.dotnet:$PATH"
```

---

## Validation

### Always Test After Changes

-   Run all unit tests after making changes to ensure nothing is broken.
-   Use the following command to run tests:

``` bash
dotnet test
```

---

## Common Tasks

### Repository Structure

    MermaidSharp/
    ├── src/
    │   ├── MermaidSharp/                                 # Core library (Framework 4.8 / .NET 8)
    │   ├── MermaidSharp.Tests/                           # Unit tests (.NET 8)  
    │   ├── MermaidSharp.MVCWeb/                          # Sample MVC app (.NET 9)
    │   ├── MermaidSharp.BlazorApp/                       # Sample Blazor app (.NET 9)
    │   ├── MermaidSharp.EntityFrameworkCore/             # Entity Framework Core (Framework 4.8 / .NET 8)
    │   ├── MermaidSharp.EntityFramework.Tests/           # Entity Framework tests (Framework 4.8)
    │   ├── MermaidSharp.EntityFrameworkCore.Tests/       # Entity Framework Core tests (.NET 8)
    │   └── MermaidSharp.sln                              # Solution file
    └── README.md                                         # Documentation

### Key Project Files

-   **Core Library**: `src/MermaidSharp/MermaidSharp.csproj`
-   **Tests**: `src/MermaidSharp.Tests/MermaidSharp.Tests.csproj`\
-   **Solution**: `src/MermaidSharp.sln`

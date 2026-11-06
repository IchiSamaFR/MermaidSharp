# MermaidSharp

MermaidSharp is a comprehensive .NET wrapper library for creating
Mermaid diagrams with full syntax support. The solution includes a
core library (.NET Framework 4.8, .NET 8 and .NET 10), extension
libraries (AutoDiagram, Entity Framework), unit tests, and sample
applications (MVC Web and Blazor) demonstrating usage.

Always reference these instructions first and fallback to search or bash
commands only when you encounter unexpected information that does not
match the info here.

---

## Coding Standards

### General Principles

-   **Target Frameworks:** Libraries and tests target `net48`, `net8.0`
    and `net10.0`; samples target `net10.0`. Library code must compile
    with C# 7.3 (the `net48` default language version).
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
-   **Indentation:** Use 4 spaces per indentation level (no tabs).
-   **EditorConfig:** Formatting and naming rules are defined in `.editorconfig`;
    keep it as the source of truth for the IDE.
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

## Commit Messages

-   **On request:** if I ask for the commit message, provide a single commit
    message covering all the changes made during the response.
-   **Message only:** provide only the commit message text, without any git
    commands (`git add`, `git commit`, etc.).

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

- `.NET 10 SDK` - Builds every target framework (version pinned by `global.json`)
- `.NET 8 runtime` - Runs the `net8.0` tests
- `.NET Framework 4.8 Developer Pack` - Builds and runs the `net48` targets (Windows only)

``` bash
# Install the .NET 10 SDK and the .NET 8 runtime (Linux/macOS)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0 --runtime dotnet

export PATH="$HOME/.dotnet:$PATH"
```

---

## Validation

### Always Test After Changes

-   Run all unit tests after making changes to ensure nothing is broken.
-   Use the following command to run tests:

``` bash
dotnet test src/MermaidSharp.sln
```

---

## Common Tasks

### Repository Structure

    MermaidSharp/
    ├── src/
    │   ├── MermaidSharp/                                 # Core library (net48 / net8.0 / net10.0)
    │   ├── MermaidSharp.AutoDiagram/                     # Reflection-based diagrams (net48 / net8.0 / net10.0)
    │   ├── MermaidSharp.EntityFrameworkCore/             # EF6 (net48) and EF Core (net8.0 / net10.0) diagrams
    │   ├── Samples/
    │   │   ├── MermaidSharp.MVCWeb/                      # Sample MVC app (net10.0)
    │   │   └── MermaidSharp.BlazorApp/                   # Sample Blazor app (net10.0)
    │   ├── Tests/
    │   │   ├── MermaidSharp.Tests/                       # Core library tests
    │   │   ├── MermaidSharp.AutoDiagram.Tests/           # AutoDiagram tests
    │   │   ├── MermaidSharp.AutoDiagram.Tests.Models/    # Models used by AutoDiagram tests
    │   │   ├── MermaidSharp.EntityFramework.Tests/       # Entity Framework 6 tests (net48)
    │   │   └── MermaidSharp.EntityFrameworkCore.Tests/   # Entity Framework Core tests (net8.0 / net10.0)
    │   ├── Directory.Build.props                         # Shared package metadata and version
    │   ├── Directory.Packages.props                      # Central NuGet package versions
    │   └── MermaidSharp.sln                              # Solution file
    ├── global.json                                       # .NET SDK version
    ├── instructions.md                                   # Shared AI assistant instructions
    └── README.md                                         # Documentation

### Key Project Files

-   **Core Library**: `src/MermaidSharp/MermaidSharp.csproj`
-   **Tests**: `src/Tests/MermaidSharp.Tests/MermaidSharp.Tests.csproj`
-   **Solution**: `src/MermaidSharp.sln`
-   **CI**: `.github/workflows/`

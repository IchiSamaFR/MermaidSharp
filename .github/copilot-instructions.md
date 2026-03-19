# MermaidSharp

MermaidSharp is a comprehensive .NET wrapper library for creating Mermaid flowcharts with full syntax support. The solution includes a core .NET 8 library, unit tests, and sample applications (MVC Web and Blazor) demonstrating usage.

Always reference these instructions first and fallback to search or bash commands only when you encounter unexpected information that does not match the info here.

## Coding Standards

### General Principles
- **Target Framework:** All projects target .NET 8.0. Ensure compatibility with this version.
- **Language:** Use C# for all code unless otherwise specified.
- **Readability:** Write clear, self-explanatory code. Use meaningful variable, method, and class names.
- **Consistency:** Follow consistent naming conventions and code formatting throughout the solution.
- **Comments:** Add inline comments only where necessary to clarify complex logic and in english, always in english.
- **Error Handling:** Use structured exception handling. Avoid swallowing exceptions; log or rethrow as appropriate.
- **SOLID Principles:** Adhere to SOLID design principles for maintainable and extensible code.
- **Dependency Injection:** Prefer constructor injection for dependencies.
- **Async/Await:** Use asynchronous programming patterns where appropriate, especially for I/O-bound operations.
- **Magic Numbers:** Avoid magic numbers; use named constants or enums.
- **File Organization:** One class per file. Organize files into appropriate folders by feature or layer.

### Naming Conventions
- **Classes & Interfaces:** PascalCase (e.g., `UserService`, `IUserRepository`)
- **Methods & Properties:** PascalCase (e.g., `GetUserById`)
- **Private & Public variables:** camelCase (e.g., `_userId`, `UserId`)
- **Parameters:** camelCase (e.g., `userId`)
- **Constants:** PascalCase (e.g., `DefaultTimeout`)
- **Unit Test Methods:** Use descriptive names indicating the scenario and expected outcome (e.g., `GetUserById_ReturnsUser_WhenUserExists`)

### Code Style
- **Braces:** Use Allman style (braces on new lines).
- **Indentation:** Use one tabulation per indentation level.
- **Line Length:** Limit lines to 120 characters.
- **Usings:** Place `using` statements outside the namespace and remove unused usings.

---

## Copilot Usage

- **Adhere to these standards** when generating or modifying code.
- **Prefer existing patterns** and conventions found in the solution.
- **Generate code that is ready to use** and fits seamlessly into the current structure.
- **Document any deviations** from these standards in pull requests or code reviews.

---

## Unit Test Standards

### General Principles
- **Test Framework:** Use MSTest as appropriate for the project.
- **Test Naming:** Use descriptive method names: `MethodName_ExpectedBehavior_StateUnderTest`.
- **Test Structure:** Follow Arrange-Act-Assert (AAA) pattern in all tests.
- **Isolation:** Each test should be independent and not rely on the outcome of other tests.
- **Mocking:** Use mocks or fakes for external dependencies (e.g. APIs) to ensure tests are deterministic.
- 
## Working Effectively

### Prerequisites
Install the required .NET SDKs in this exact order:
- `.NET 8.0 SDK` - Required for core library and tests
- `.NET 9.0 SDK` - Required for sample applications

```bash
# Install .NET 8 first (required for tests)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 8.0

# Install .NET 9 (for sample applications)
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 9.0

export PATH="/home/runner/.dotnet:$PATH"
```

### Bootstrap, Build, and Test
Run these commands in this exact sequence:

```bash
cd /home/runner/work/MermaidSharp/MermaidSharp
export PATH="/home/runner/.dotnet:$PATH"

# Restore packages - takes 2-3 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
dotnet restore src/MermaidSharp.sln

# Build solution - takes 5-6 seconds. NEVER CANCEL. Set timeout to 5+ minutes.
dotnet build src/MermaidSharp.sln -c Release

# Run tests - takes 3-4 seconds. NEVER CANCEL. Set timeout to 2+ minutes.
dotnet test src/MermaidSharp.Tests/MermaidSharp.Tests.csproj -c Release
```

**CRITICAL**: NEVER CANCEL build or test commands. Builds may take 15-20 seconds, tests take ~4 seconds. Always use timeouts of 5+ minutes for builds and 2+ minutes for tests.

### Core Library Only (Faster)
For development focused on the core library without sample apps:

```bash
# Core library restore - takes 1 second
dotnet restore src/MermaidSharp/MermaidSharp.csproj

# Core library build - takes 2 seconds 
dotnet build src/MermaidSharp/MermaidSharp.csproj -c Release

# Run tests - takes 3-4 seconds
dotnet test src/MermaidSharp.Tests/MermaidSharp.Tests.csproj -c Release
```

### Run Sample Applications
Both sample applications demonstrate the library in action:

**MVC Web Application:**
```bash
cd src/MermaidSharp.MVCWeb
export PATH="/home/runner/.dotnet:$PATH"
dotnet run --urls=http://localhost:5000
# Access at http://localhost:5000
```

**Blazor WebAssembly Application:**
```bash
cd src/MermaidSharp.BlazorApp  
export PATH="/home/runner/.dotnet:$PATH"
dotnet run --urls=http://localhost:5001
# Access at http://localhost:5001
```

### NuGet Package Creation
```bash
# Create NuGet package - takes 1-2 seconds
dotnet pack src/MermaidSharp/MermaidSharp.csproj -c Release
# Output: src/MermaidSharp/bin/Release/MermaidSharp.{version}.nupkg
```

## Validation

### Always Test After Changes
Run this complete validation sequence after making any code changes:

1. **Build Validation:**
   ```bash
   dotnet build src/MermaidSharp.sln -c Release
   # Expected: Success with XML documentation warnings (58 warnings normal)
   ```

2. **Test Validation:**
   ```bash
   dotnet test src/MermaidSharp.Tests/MermaidSharp.Tests.csproj -c Release
   # Expected: 43 tests pass, 0 failures
   ```

3. **Sample Application Validation:**
   ```bash
   # Start MVC app
   cd src/MermaidSharp.MVCWeb && dotnet run --urls=http://localhost:5000 &
   
   # Test response
   curl -I http://localhost:5000
   # Expected: HTTP/1.1 200 OK
   
   # Start Blazor app  
   cd ../MermaidSharp.BlazorApp && dotnet run --urls=http://localhost:5001 &
   
   # Test response
   curl -I http://localhost:5001  
   # Expected: HTTP/1.1 200 OK
   ```

4. **Manual Functional Testing:**
   - ALWAYS manually test flowchart generation by creating a simple test
   - Verify Mermaid syntax output matches expected format
   - Test both basic and advanced node types/link types

### Example Manual Test
Create this test to validate core functionality works correctly:
```csharp
using MermaidSharp.Models;

[TestMethod]
public void BasicFlowchartValidation()
{
    // Arrange - Create a simple flowchart
    var nodes = new List<Node>
    {
        new("start", "Start", Node.ShapeType.Circle),
        new("process", "Process Data", Node.ShapeType.Rectangle),
        new("end", "End", Node.ShapeType.Stadium)
    };
    var links = new List<Link>
    {
        new Link("start", "process", "begin"),
        new Link("process", "end", "complete")
    };
    var flowchart = new Flowchart("LR", nodes, links);
    
    // Act - Generate Mermaid syntax
    string result = flowchart.CalculateFlowchart();
    
    // Assert - Verify valid Mermaid syntax output
    Assert.IsTrue(result.Contains("flowchart LR"));
    Assert.IsTrue(result.Contains("start((Start))"));
    Assert.IsTrue(result.Contains("process[Process Data]"));
    Assert.IsTrue(result.Contains("end([End])"));
    Assert.IsTrue(result.Contains("start--begin-->process"));
    Assert.IsTrue(result.Contains("process--complete-->end"));
}
```

## Common Tasks

### Repository Structure
```
MermaidSharp/
├── src/
│   ├── MermaidSharp/              # Core library (.NET 8)
│   ├── MermaidSharp.Tests/        # Unit tests (.NET 8)  
│   ├── MermaidSharp.MVCWeb/       # Sample MVC app (.NET 9)
│   ├── MermaidSharp.BlazorApp/    # Sample Blazor app (.NET 9)
│   └── MermaidSharp.sln           # Solution file
├── .github/workflows/workflow.yml  # CI/CD pipeline
├── GitVersion.yml                  # Version configuration
└── README.md                       # Documentation
```

### Key Project Files
- **Core Library**: `src/MermaidSharp/MermaidSharp.csproj`
- **Tests**: `src/MermaidSharp.Tests/MermaidSharp.Tests.csproj`  
- **Solution**: `src/MermaidSharp.sln`

### Common Scenarios

**Adding New Node Types:**
1. Update `Node.cs` ShapeType enum
2. Update `Node.OpenShape()` and `Node.CloseShape()` methods  
3. Add unit tests in `MermaidSharp.Tests`
4. Run full validation sequence

**Adding New Link Types:**
1. Update `Link.cs` LinkType enum
2. Update flowchart generation logic in `Flowchart.cs`
3. Add unit tests
4. Run full validation sequence

**Testing Sample Applications:**
- Both applications include working examples of the library
- MVC app demonstrates server-side rendering
- Blazor app demonstrates client-side usage
- Always test both after core library changes

## Build Warnings Expected
The build produces 58-60 XML documentation warnings - this is normal and expected. The library builds successfully despite these warnings.

## CI/CD Integration
The project uses GitHub Actions for:
- Automated building and testing
- Code coverage via Coveralls
- SonarCloud analysis  
- NuGet package publishing
- Automatic releases via GitVersion

Always ensure your changes pass local validation before pushing to avoid CI failures.

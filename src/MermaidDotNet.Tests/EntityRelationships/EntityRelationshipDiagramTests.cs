using MermaidDotNet.Diagrams;
using MermaidDotNet.Enums;
using MermaidDotNet.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MermaidDotNet.Tests.EntityRelationships
{
    [TestClass]
    public class EntityRelationshipDiagramTests
    {
        [TestMethod]
        public void Constructor_ShouldInitializeNodesAndLinks_WithComplexDatabase()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("User", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("Id", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Name", "string"),
                    new EntityRelationColumn("Email", "string")
                }),
                new EntityRelationNode("Order", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("UserId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("OrderDate", "datetime")
                }),
                new EntityRelationNode("Product", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Title", "string"),
                    new EntityRelationColumn("Price", "decimal")
                }),
                new EntityRelationNode("OrderItem", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("OrderItemId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("OrderId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("ProductId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("Quantity", "int")
                }),
                new EntityRelationNode("Category", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CategoryId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("ProductCategory", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProductId", "int", ColumnKeyType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("CategoryId", "int", ColumnKeyType.PrimaryKeyForeignKey)
                })
            };

            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("User", "Order", "UserId (Cascade)", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Order", "OrderItem", "OrderId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Product", "OrderItem", "ProductId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Product", "ProductCategory", "ProductId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Category", "ProductCategory", "CategoryId", RelationType.OneOrMore, RelationType.ZeroOrMore)
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"erDiagram
    User {
        int Id PK
        string Name
        string Email
    }
    Order {
        int OrderId PK
        int UserId FK
        datetime OrderDate
    }
    Product {
        int ProductId PK
        string Title
        decimal Price
    }
    OrderItem {
        int OrderItemId PK
        int OrderId FK
        int ProductId FK
        int Quantity
    }
    Category {
        int CategoryId PK
        string Name
    }
    ProductCategory {
        int ProductId PK, FK
        int CategoryId PK, FK
    }
    User }|--o{ Order : ""UserId (Cascade)""
    Order }|--o{ OrderItem : ""OrderId""
    Product }|--o{ OrderItem : ""ProductId""
    Product }|--o{ ProductCategory : ""ProductId""
    Category }|--o{ ProductCategory : ""CategoryId""";

            // Act;
            string result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldReturnEmptyDiagram_WhenNoNodesOrLinks()
        {
            // Arrange
            var diagram = new EntityRelationshipDiagram();

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.AreEqual("erDiagram", diagram.Name);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleSingleNodeWithoutLinks()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Customer", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CustomerId", "int"),
                    new EntityRelationColumn("FullName", "string")
                })
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);

            var expected = @"erDiagram
    Customer {
        int CustomerId
        string FullName
    }";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleCircularRelations()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Person", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("PersonId", "int", ColumnKeyType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("ManagerId", "int", ColumnKeyType.PrimaryKeyForeignKey)
                })
            };
            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("Person", "Person", "ManagerId", RelationType.ZeroOrOne, RelationType.ZeroOrMore)
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"erDiagram
    Person {
        int PersonId PK, FK
        int ManagerId PK, FK
    }
    Person |o--o{ Person : ""ManagerId""";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleMultipleLinksBetweenSameEntities()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Author", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("AuthorId", "int"),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("Book", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("BookId", "int"),
                    new EntityRelationColumn("Title", "string"),
                    new EntityRelationColumn("AuthorId", "int"),
                    new EntityRelationColumn("EditorId", "int")
                })
            };
            var links = new List<EntityRelationLink>
            {
                new EntityRelationLink("Author", "Book", "AuthorId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Author", "Book", "EditorId", RelationType.OneOrMore, RelationType.ZeroOrMore)
            };
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var expected = @"erDiagram
    Author {
        int AuthorId
        string Name
    }
    Book {
        int BookId
        string Title
        int AuthorId
        int EditorId
    }
    Author }|--o{ Book : ""AuthorId""
    Author }|--o{ Book : ""EditorId""";

            // Act
            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void CalculateDiagram_ShouldHandleVariousRelationTypesAndEdgeCases()
        {
            // Arrange
            var nodes = new List<EntityRelationNode>
            {
                new EntityRelationNode("Person", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("PersonId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("SpouseId", "int", ColumnKeyType.ForeignKey)
                }),
                new EntityRelationNode("Company", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("CompanyId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("Employee", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("EmployeeId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("PersonId", "int", ColumnKeyType.ForeignKey),
                    new EntityRelationColumn("CompanyId", "int", ColumnKeyType.ForeignKey)
                }),
                new EntityRelationNode("Project", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("ProjectId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Title", "string")
                }),
                new EntityRelationNode("EmployeeProject", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("EmployeeId", "int", ColumnKeyType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("ProjectId", "int", ColumnKeyType.PrimaryKeyForeignKey)
                }),
                new EntityRelationNode("Department", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("DepartmentId", "int", ColumnKeyType.PrimaryKey),
                    new EntityRelationColumn("Name", "string")
                }),
                new EntityRelationNode("DepartmentManager", new List<EntityRelationColumn>
                {
                    new EntityRelationColumn("DepartmentId", "int", ColumnKeyType.PrimaryKeyForeignKey),
                    new EntityRelationColumn("PersonId", "int", ColumnKeyType.PrimaryKeyForeignKey)
                })
            };

            var links = new List<EntityRelationLink>
            {
		        // One-to-one (circular)
		        new EntityRelationLink("Person", "Person", "SpouseId", RelationType.ZeroOrOne, RelationType.ZeroOrOne),
		        // One-to-many
		        new EntityRelationLink("Company", "Employee", "CompanyId", RelationType.OneOrMore, RelationType.ZeroOrMore),
		        // Many-to-many
		        new EntityRelationLink("Employee", "EmployeeProject", "EmployeeId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Project", "EmployeeProject", "ProjectId", RelationType.OneOrMore, RelationType.ZeroOrMore),
		        // Multiple links between same entities
		        new EntityRelationLink("Department", "DepartmentManager", "DepartmentId", RelationType.OneOrMore, RelationType.ZeroOrMore),
                new EntityRelationLink("Person", "DepartmentManager", "PersonId", RelationType.OneOrMore, RelationType.ZeroOrMore),
		        // No foreign key relation
		        new EntityRelationLink("Employee", "Department", "DepartmentId", RelationType.ZeroOrMore, RelationType.ZeroOrMore)
            };

            var expected = @"erDiagram
    Person {
        int PersonId PK
        int SpouseId FK
    }
    Company {
        int CompanyId PK
        string Name
    }
    Employee {
        int EmployeeId PK
        int PersonId FK
        int CompanyId FK
    }
    Project {
        int ProjectId PK
        string Title
    }
    EmployeeProject {
        int EmployeeId PK, FK
        int ProjectId PK, FK
    }
    Department {
        int DepartmentId PK
        string Name
    }
    DepartmentManager {
        int DepartmentId PK, FK
        int PersonId PK, FK
    }
    Person |o--o| Person : ""SpouseId""
    Company }|--o{ Employee : ""CompanyId""
    Employee }|--o{ EmployeeProject : ""EmployeeId""
    Project }|--o{ EmployeeProject : ""ProjectId""
    Department }|--o{ DepartmentManager : ""DepartmentId""
    Person }|--o{ DepartmentManager : ""PersonId""
    Employee }o--o{ Department : ""DepartmentId""";

            // Act
            var diagram = new EntityRelationshipDiagram();
            diagram.Nodes.AddRange(nodes);
            diagram.Links.AddRange(links);

            var result = diagram.CalculateDiagram();

            // Assert
            Assert.IsNotNull(diagram);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}

using MermaidSharp.EntityFrameworkCore.Tests.Mock;
using Microsoft.EntityFrameworkCore;

namespace MermaidSharp.EntityFrameworkCore.Tests
{
    [TestClass]
    public class ArgumentValidationTests
    {
        [TestMethod]
        public void ToMermaidEntityDiagram_Throws_WhenContextIsNull()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => ((DbContext)null!).ToMermaidEntityDiagram());
        }

        [TestMethod]
        public void ToMermaidEntityDiagram_UsesDefaultOptions_WhenOptionsAreNull()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DatabaseContextMock>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using var context = new DatabaseContextMock(options);

            // Act
            var expected = context.ToMermaidEntityDiagram().CalculateDiagram();
            var result = context.ToMermaidEntityDiagram(null!).CalculateDiagram();

            // Assert
            Assert.AreEqual(expected, result);
        }
    }
}

namespace MermaidSharp.Samples.Tests
{
    /// <summary>
    /// Checks properties that every sample diagram of the catalog must satisfy.
    /// </summary>
    [TestClass]
    public class DiagramCatalogTests
    {
        private const string FrontMatterSeparator = "---";
        private const string Indentation = "    ";

        /// <summary>
        /// Gets the identifier of every sample, one test case per sample.
        /// </summary>
        public static IEnumerable<object[]> ExampleIds => DiagramCatalog.All.Select(e => new object[] { e.Id });

        [TestMethod]
        public void Catalog_ContainsAtLeastOneExample_ForEveryDiagramKind()
        {
            // Act
            var missingKinds = Enum.GetValues<DiagramKind>()
                .Where(kind => DiagramCatalog.All.All(e => e.Kind != kind))
                .ToList();

            // Assert
            Assert.AreEqual(0, missingKinds.Count, $"Missing examples for: {string.Join(", ", missingKinds)}");
        }

        [TestMethod]
        public void Catalog_HasUniqueIds()
        {
            // Act
            var duplicates = DiagramCatalog.All
                .GroupBy(e => e.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            // Assert
            Assert.AreEqual(0, duplicates.Count, $"Duplicate ids: {string.Join(", ", duplicates)}");
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Example_HasTitleDescriptionAndDefinition(string id)
        {
            // Arrange
            var example = Get(id);

            // Assert
            Assert.IsFalse(string.IsNullOrWhiteSpace(example.Title));
            Assert.IsFalse(string.IsNullOrWhiteSpace(example.Description));
            Assert.IsFalse(string.IsNullOrWhiteSpace(example.Definition));
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_StartsWithKeywordOfItsDiagramKind(string id)
        {
            // Arrange
            var example = Get(id);

            // Act
            var keywordLine = GetBody(example.Definition).First();

            // Assert
            var expectedKeywords = GetExpectedKeywords(example.Kind);
            Assert.IsTrue(expectedKeywords.Any(k => keywordLine == k || keywordLine.StartsWith(k + " ", StringComparison.Ordinal)),
                $"'{keywordLine}' does not start with one of: {string.Join(", ", expectedKeywords)}");
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_FrontMatterIsClosedAndIndented(string id)
        {
            // Arrange
            var lines = GetLines(Get(id).Definition);
            if (lines[0] != FrontMatterSeparator)
                return;

            // Act
            var closingIndex = Array.IndexOf(lines, FrontMatterSeparator, 1);

            // Assert
            Assert.IsTrue(closingIndex > 1, "The front matter is not closed or is empty.");
            foreach (var line in lines.Skip(1).Take(closingIndex - 1))
            {
                StringAssert.Contains(line, ":", $"Front matter line is not a YAML key: '{line}'");
            }
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_BodyIsIndentedUnderKeyword(string id)
        {
            // Act
            var body = GetBody(Get(id).Definition).Skip(1).ToList();

            // Assert
            Assert.IsTrue(body.Count > 0, "The diagram has no content.");
            foreach (var line in body)
            {
                Assert.IsTrue(line.StartsWith(Indentation, StringComparison.Ordinal), $"Line is not indented: '{line}'");
            }
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_HasNoEmptyLinesTabsOrTrailingWhitespace(string id)
        {
            // Arrange
            var lines = GetLines(Get(id).Definition);

            // Assert
            foreach (var line in lines)
            {
                Assert.IsFalse(string.IsNullOrWhiteSpace(line), "The definition contains an empty line.");
                Assert.IsFalse(line.Contains('\t'), $"Line contains a tab: '{line}'");
                Assert.AreEqual(line.TrimEnd(), line, $"Line has trailing whitespace: '{line}'");
            }
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_HasBalancedQuotesOnEveryLine(string id)
        {
            // Arrange
            var lines = GetLines(Get(id).Definition);

            // Assert
            foreach (var line in lines)
            {
                // Escaped quotes (\") belong to YAML strings and do not open or close a string.
                var quoteCount = line.Replace("\\\"", string.Empty).Count(c => c == '"');
                Assert.AreEqual(0, quoteCount % 2, $"Unbalanced quotes: '{line}'");
            }
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_IsDeterministic(string id)
        {
            // Act
            var regenerated = DiagramCatalog.Create().Single(e => e.Id == id);

            // Assert
            Assert.AreEqual(Get(id).Definition, regenerated.Definition);
        }

        [TestMethod]
        [DynamicData(nameof(ExampleIds))]
        public void Definition_SubgraphIdsDoNotCollideWithNodeIds(string id)
        {
            // Arrange
            var body = GetBody(Get(id).Definition).ToList();
            if (!body[0].StartsWith("flowchart", StringComparison.Ordinal))
                return;

            var subgraphIds = body
                .Select(l => l.Trim())
                .Where(l => l.StartsWith("subgraph ", StringComparison.Ordinal))
                .Select(l => l.Substring("subgraph ".Length).Split('[', ' ')[0])
                .ToList();
            var nodeIds = body
                .Select(l => l.Trim())
                .Where(l => !l.StartsWith("subgraph ", StringComparison.Ordinal))
                .Select(l => System.Text.RegularExpressions.Regex.Match(l, @"^[^\s\[\(\{>\-=~.]+(?:\.[^\s\[\(\{>\-=~.]+)*").Value)
                .Where(v => v.Length > 0)
                .ToHashSet();

            // Assert: Mermaid fails to render a subgraph that shares its id with a node (cycle).
            var collisions = subgraphIds.Where(nodeIds.Contains).ToList();
            Assert.AreEqual(0, collisions.Count, $"Subgraph ids used as node ids: {string.Join(", ", collisions)}");
        }

        private static DiagramExample Get(string id)
        {
            return DiagramCatalog.All.Single(e => e.Id == id);
        }

        private static string[] GetLines(string definition)
        {
            return definition.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
        }

        /// <summary>
        /// Returns the lines after the optional front matter block, starting with the diagram keyword.
        /// </summary>
        private static IEnumerable<string> GetBody(string definition)
        {
            var lines = GetLines(definition);
            if (lines[0] != FrontMatterSeparator)
                return lines;

            var closingIndex = Array.IndexOf(lines, FrontMatterSeparator, 1);
            return lines.Skip(closingIndex + 1);
        }

        private static string[] GetExpectedKeywords(DiagramKind kind)
        {
            return kind switch
            {
                DiagramKind.Flowchart => new[] { "flowchart" },
                DiagramKind.ClassDiagram => new[] { "classDiagram" },
                DiagramKind.EntityRelationship => new[] { "erDiagram" },
                DiagramKind.GitGraph => new[] { "gitGraph" },
                DiagramKind.PieChart => new[] { "pie" },
                DiagramKind.QuadrantChart => new[] { "quadrantChart" },
                DiagramKind.XYChart => new[] { "xychart" },
                DiagramKind.AutoDiagram => new[] { "classDiagram", "flowchart" },
                _ => throw new ArgumentOutOfRangeException(nameof(kind), kind, null)
            };
        }
    }
}

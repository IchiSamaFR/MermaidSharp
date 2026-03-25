using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MermaidSharp.Tests.GitGraphs
{
    [TestClass]
    public class GitCommitTests
    {
        [TestMethod]
        public void GitCommit()
        {
            //Arrange
            var commit = new GitCommit();
            string expected = @"commit";

            //Act
            string result = commit.ToString();

            //Assert
            Assert.IsNotNull(commit);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitCommit_Id()
        {
            //Arrange
            var commit = new GitCommit("my commit desc");
            string expected = @"commit id: ""my commit desc""";

            //Act
            string result = commit.ToString();

            //Assert
            Assert.IsNotNull(commit);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitCommit_Tag()
        {
            //Arrange
            var commit = new GitCommit(tag: "my tag");
            string expected = @"commit tag: ""my tag""";

            //Act
            string result = commit.ToString();

            //Assert
            Assert.IsNotNull(commit);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }


        [TestMethod]
        public void GitCommit_IdAndTag()
        {
            //Arrange
            var commit = new GitCommit("my commit desc", "my tag");
            string expected = @"commit id: ""my commit desc"" tag: ""my tag""";

            //Act
            string result = commit.ToString();

            //Assert
            Assert.IsNotNull(commit);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}

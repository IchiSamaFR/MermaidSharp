using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MermaidSharp.Tests.GitGraphs
{
    [TestClass]
    public class GitMergeTests
    {
        [TestMethod]
        public void GitMerge()
        {
            //Arrange
            var merge = new GitMerge("myBranch");
            string expected = @"merge myBranch";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void GitMerge_Tag()
        {
            //Arrange
            var merge = new GitMerge("myBranch", "my tag");
            string expected = @"merge myBranch tag: ""my tag""";

            //Act
            string result = merge.ToString();

            //Assert
            Assert.IsNotNull(merge);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}

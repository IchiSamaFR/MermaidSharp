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
    public class GitBranchTests
    {
        [TestMethod]
        public void GitBranch()
        {
            //Arrange
            var branch = new GitBranch("myBranch");
            string expected = @"branch myBranch";

            //Act
            string result = branch.ToString();

            //Assert
            Assert.IsNotNull(branch);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}

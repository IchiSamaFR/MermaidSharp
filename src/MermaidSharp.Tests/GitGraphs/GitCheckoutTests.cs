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
    public class GitCheckoutTests
    {
        [TestMethod]
        public void GitCheckout()
        {
            //Arrange
            var checkout = new GitCheckout("myBranch");
            string expected = @"checkout myBranch";

            //Act
            string result = checkout.ToString();

            //Assert
            Assert.IsNotNull(checkout);
            Assert.IsNotNull(result);
            Assert.AreEqual(expected, result);
        }
    }
}

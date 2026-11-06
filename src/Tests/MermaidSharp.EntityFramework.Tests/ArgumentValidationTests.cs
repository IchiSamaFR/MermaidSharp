using MermaidSharp.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity;

namespace MermaidSharp.EntityFramework.Tests
{
    [TestClass]
    public class ArgumentValidationTests
    {
        [TestMethod]
        public void ToMermaidEntityDiagram_Throws_WhenContextIsNull()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => ((DbContext)null).ToMermaidEntityDiagram());
        }
    }
}

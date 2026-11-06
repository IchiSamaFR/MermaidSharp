using MermaidSharp.AutoDiagram.Diagrams;
using MermaidSharp.AutoDiagram.Enums;
using MermaidSharp.AutoDiagram.Options;
using MermaidSharp.AutoDiagram.Tests.ClassDiagrams.Fixtures;
using MermaidSharp.AutoDiagram.Tests.Models;
using MermaidSharp.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MermaidSharp.AutoDiagram.Tests.ClassDiagrams
{
    /// <summary>
    /// Tests for the options of <see cref="ClassDiagramExtension"/>.
    /// </summary>
    [TestClass]
    public class ClassDiagramOptionsTests
    {
        private static readonly Type[] HierarchyTypes =
        {
            typeof(Person), typeof(Employee), typeof(Manager), typeof(IContactable), typeof(Department<>)
        };

        [TestMethod]
        public void ToMermaidClassDiagram_ExcludesNonPublicProperties_WhenOnlyPublicVisibilityIsIncluded()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                PropertyOptions = new ClassPropertyOptions
                {
                    IncludeVisibility = new List<ClassPropertyVisibility> { ClassPropertyVisibility.Public }
                }
            };

            // Act
            var result = typeof(Person).ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "+String FirstName");
            StringAssert.Contains(result, "+String LastName");
            Assert.IsFalse(result.Contains("SecretCode"), result);
            Assert.IsFalse(result.Contains("InternalId"), result);
            Assert.IsFalse(result.Contains("Int32 Age"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_ExcludesNonPublicMethods_WhenOnlyPublicVisibilityIsIncluded()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                MethodOptions = new ClassMethodOptions
                {
                    IncludeVisibility = new List<ClassPropertyVisibility> { ClassPropertyVisibility.Public }
                }
            };

            // Act
            var result = typeof(Person).ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "+SayHello()");
            Assert.IsFalse(result.Contains("Hide()"), result);
            Assert.IsFalse(result.Contains("DoWork()"), result);
            Assert.IsFalse(result.Contains("InternalMethod()"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_ExcludesParameters_WhenIncludeParametersIsFalse()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                MethodOptions = new ClassMethodOptions { IncludeParameters = false }
            };

            // Act
            var result = typeof(Employee).ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "+Contact()");
            Assert.IsFalse(result.Contains("Contact(String)"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_WritesReturnType_ExceptVoidByDefault()
        {
            // Act
            var result = typeof(Calculator).ToMermaidClassDiagram().CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "+Add(Int32, Int32) Int32");
            StringAssert.Contains(result, "+Reset()");
            Assert.IsFalse(result.Contains("Reset() Void"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_WritesVoid_WhenIncludeReturnVoidIsTrue()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                MethodOptions = new ClassMethodOptions { IncludeReturnVoid = true }
            };

            // Act
            var result = typeof(Calculator).ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "+Reset() Void");
        }

        [TestMethod]
        public void ToMermaidClassDiagram_ExcludesReturnTypes_WhenIncludeReturnTypeIsFalse()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                MethodOptions = new ClassMethodOptions { IncludeReturnType = false }
            };

            // Act
            var result = typeof(Calculator).ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "+Add(Int32, Int32)");
            Assert.IsFalse(result.Contains("+Add(Int32, Int32) Int32"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_WritesNoLinks_WhenIncludeLinksIsNone()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                LinkOptions = new ClassLinkOptions { IncludeLinks = ClassLinkOption.None }
            };

            // Act
            var result = HierarchyTypes.ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            Assert.IsFalse(result.Contains("<|--"), result);
            Assert.IsFalse(result.Contains("..|>"), result);
            Assert.IsFalse(result.Contains("-->"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_WritesOnlyInheritance_WhenIncludeLinksIsInherited()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                LinkOptions = new ClassLinkOptions { IncludeLinks = ClassLinkOption.Inherited }
            };

            // Act
            var result = HierarchyTypes.ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "Person<|--Employee : Inherited");
            StringAssert.Contains(result, "Employee<|--Manager : Inherited");
            Assert.IsFalse(result.Contains(": Association"), result);
            Assert.IsFalse(result.Contains(": Interface"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_WritesLinksWithoutLabels_WhenIncludeLinksLabelsIsFalse()
        {
            // Arrange
            var options = new ClassDiagramOptions
            {
                LinkOptions = new ClassLinkOptions { IncludeLinksLabels = false }
            };

            // Act
            var result = HierarchyTypes.ToMermaidClassDiagram(options).CalculateDiagram();

            // Assert
            StringAssert.Contains(result, "Person<|--Employee");
            StringAssert.Contains(result, "IContactable..|>Employee");
            Assert.IsFalse(result.Contains(" : Inherited"), result);
            Assert.IsFalse(result.Contains(" : Interface"), result);
            Assert.IsFalse(result.Contains(" : Association"), result);
        }

        [TestMethod]
        public void ToMermaidClassDiagram_Throws_WhenSourceIsNull()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => ((Type)null).ToMermaidClassDiagram());
            Assert.ThrowsExactly<ArgumentNullException>(() => ((IEnumerable<Type>)null).ToMermaidClassDiagram());
            Assert.ThrowsExactly<ArgumentNullException>(() => ((Assembly)null).ToMermaidClassDiagram());
            Assert.ThrowsExactly<ArgumentNullException>(() => ((IEnumerable<Assembly>)null).ToMermaidClassDiagram());
        }
    }
}

using MermaidSharp.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MermaidSharp.Tests.PieCharts
{
	[TestClass]
	public class PieSliceTests
	{
		[TestMethod]
		public void PieSlice_ToString_WithIntegerValue()
		{
			// Arrange
			var slice = new PieSlice("Dogs", 386);
			string expected = "\"Dogs\" : 386";

			// Act
			string result = slice.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void PieSlice_ToString_WithDecimalValue()
		{
			// Arrange
			var slice = new PieSlice("Cats", 42.96);
			string expected = "\"Cats\" : 42.96";

			// Act
			string result = slice.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void PieSlice_ToString_WithSingleDecimalValue()
		{
			// Arrange
			var slice = new PieSlice("Rats", 10.5);
			string expected = "\"Rats\" : 10.5";

			// Act
			string result = slice.ToString();

			// Assert
			Assert.IsNotNull(result);
			Assert.AreEqual(expected, result);
		}

		[TestMethod]
		public void PieSlice_Constructor_ThrowsArgumentOutOfRangeException_WhenValueIsZero()
		{
			// Arrange
			try
			{
				// Act
				var slice = new PieSlice("Dogs", 0);

				// Assert
				Assert.Fail("Expected ArgumentOutOfRangeException was not thrown.");
			}
			catch (ArgumentOutOfRangeException ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		[TestMethod]
		public void PieSlice_Constructor_ThrowsArgumentOutOfRangeException_WhenValueIsNegative()
		{
			// Arrange
			try
			{
				// Act
				var slice = new PieSlice("Dogs", -5);

				// Assert
				Assert.Fail("Expected ArgumentOutOfRangeException was not thrown.");
			}
			catch (ArgumentOutOfRangeException ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		[TestMethod]
		public void PieSlice_Constructor_ThrowsArgumentNullException_WhenLabelIsNull()
		{
			// Arrange
			try
			{
				// Act
				var slice = new PieSlice(null, 42);

				// Assert
				Assert.Fail("Expected ArgumentNullException was not thrown.");
			}
			catch (ArgumentNullException ex)
			{
				Assert.IsNotNull(ex);
			}
		}

		[TestMethod]
		public void PieSlice_Properties_AreSetCorrectly()
		{
			// Arrange
			var slice = new PieSlice("Birds", 25.5);

			// Assert
			Assert.AreEqual("Birds", slice.Label);
			Assert.AreEqual(25.5, slice.Value);
		}
	}
}

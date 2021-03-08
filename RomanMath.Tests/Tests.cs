using NUnit.Framework;
using RomanMath.Impl;

namespace RomanMath.Tests
{
	public class Tests
	{
		[SetUp]
		public void Setup()
		{
		}

		[Test]
		public void TestToInt()
		{
			//arrange
			string romanNumber = "XIV";
			int expected = 14;

			//act
			int actual = Service.ToInt(romanNumber.ToCharArray());

			//assert
			Assert.AreEqual(expected, actual, "{0} must be {1}", romanNumber, expected);
		}

		[Test]
		public void TestRemoveAllSpaces()
		{
			//arrange
			string romanNumbers = "IV  II+CX V  X";
			string expected = "IVII+CXVX";

			//act
			string actual = Service.RemoveAllSpaces(romanNumbers.ToCharArray());

			//assert
			Assert.AreEqual(expected, actual, "{0} must be {1}", romanNumbers, expected);
		}

		[Test]
		public void TestIsOperator()
		{
			//arrange
			char symbol1 = 'R';
			bool expected1 = false;
			char symbol2 = '+';
			bool expected2 = true;

			//act
			bool actual1 = Service.IsOperator(symbol1);
			bool actual2 = Service.IsOperator(symbol2);

			//assert
			Assert.AreEqual(expected1, actual1, "{0} must be {1}", symbol1, expected1);
			Assert.AreEqual(expected2, actual2, "{0} must be {1}", symbol2, expected2);
		}

		[Test]
		public void TestGetPostfixExpression()
		{
			//arrange
			string expression = "4+5*2";
			string expected = "4 5 2 * + ";

			//act
			string actual = Service.GetPostfixExpression(expression);

			//assert
			Assert.AreEqual(expected, actual, "{0} must be {1}", expression, expected);
		}

		[Test]
		public void TestCounting()
		{
			//arrange
			string expression = "4 5 2 * + ";
			int expected = 14;

			//act
			int actual = Service.Counting(expression);

			//assert
			Assert.AreEqual(expected, actual, "{0} must be {1}", expression, expected);
		}

		[Test]
		public void TestToNormalNumbers()
		{
			//arrange
			string[] expression = { "V","*","XIV","-","C","/","X" };
			string expected = "5*14-100/10";

			//act
			string actual = Service.ToNormalNumbers(expression);

			//assert
			Assert.AreEqual(expected, actual, "{0} must be {1}", expression, expected);
		}
	}
}
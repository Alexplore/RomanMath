using System;
using System.Collections.Generic;

namespace RomanMath.Impl
{
	public static class Service
	{
		/// <summary>
		/// See TODO.txt file for task details.
		/// Do not change contracts: input and output arguments, method name and access modifiers
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>

		public static int Evaluate(string expression)
		{
			if (expression != null) 
			{
				expression = expression.ToUpper();
				expression = RemoveAllSpaces(expression.ToCharArray());
				expression = AddSpaces(expression.ToCharArray());
				string[] expressionMembers = expression.Split(' ');
				expression = ToNormalNumbers(expressionMembers);
				expression = GetPostfixExpression(expression);
				return Counting(expression);
			}
			throw new ArgumentNullException();
		}

		/// <summary>
		/// Converts roman numbers to int
		/// </summary>
		/// <param name="romanDigits"></param>
		/// <returns></returns>
		public static int ToInt(char[] romanDigits) {
			int number = 0;
            for (int i = 0; i < romanDigits.Length; i++)
            {
				if (romanDigits.Length - i > 3)
				{
					//if there are four I or X or C or M in line 
					if ((ToInt(romanDigits[i]) == 1 || ToInt(romanDigits[i]) == 10 || ToInt(romanDigits[i]) == 100 ||
							ToInt(romanDigits[i]) == 1000))
					{
						int countEquals = 0;
						for (int j = i; j < i + 3; j++)
						{
							if (ToInt(romanDigits[i]) == ToInt(romanDigits[j]))
							{
								countEquals++;
							}
						}
						if (countEquals == 4)
						{
							throw new Exception("Incorrect format of roman number!");
						}
					}
				}
				if(romanDigits.Length - i > 1)
				{
					//if there are two V or L or D
					if ((ToInt(romanDigits[i]) == 5 || ToInt(romanDigits[i]) == 50 || ToInt(romanDigits[i]) == 500))
					{
						if (ToInt(romanDigits[i]) == ToInt(romanDigits[i + 1]))
						{
							throw new Exception("Incorrect format of roman number!");
						}
					}
					if (ToInt(romanDigits[i]) < ToInt(romanDigits[i+1]) && ToInt(romanDigits[i+1])/ ToInt(romanDigits[i]) != 10 &&
						ToInt(romanDigits[i+1]) != 5) {
						throw new Exception("Incorrect format of roman number!");
					}
				}
				if ( romanDigits.Length - i > 2)
				{
					if (ToInt(romanDigits[i]) < ToInt(romanDigits[i + 2])) {
						throw new Exception("Incorrect format of roman number!");
					}
				}
				if (i != romanDigits.Length - 1)
				{
					if (ToInt(romanDigits[i]) < ToInt(romanDigits[i + 1])) 
					{
						number += ToInt(romanDigits[i + 1]) - ToInt(romanDigits[i]);
						i++;
					}
					else
					{
						number += ToInt(romanDigits[i]);
					}
				}
				else
				{
					number += ToInt(romanDigits[i]);
				}
			}
			return number;
		}

		/// <summary>
		/// Converts roman numeral to int
		/// </summary>
		/// <param name="romanDigit"></param>
		/// <returns></returns>
		public static int ToInt(char romanDigit) {
            switch (romanDigit)
            {
				case 'I':
					return 1;
				case 'V':
					return 5;
				case 'X':
					return 10;
				case 'L':
					return 50;
				case 'C':
					return 100;
				case 'D':
					return 500;
				case 'M':
					return 1000;
				default:
					throw new Exception("Expression contains illegal elements!");
			}
        }

		/// <summary>
		/// Adds to expression spaces between the numbers and mathematical signs
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string AddSpaces(char[] expression) {
			string expressionWithSpaces = "";
            for (int i = 0; i < expression.Length; i++)
            {
				if (char.IsLetter(expression[i]))
				{
					expressionWithSpaces += expression[i];
				}
				else 
				{
					expressionWithSpaces += $" {expression[i]} ";
				}
            }
			return expressionWithSpaces;
		}

		/// <summary>
		/// Removes from expression all spaces
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string RemoveAllSpaces(char[] expression)
		{
			string expressionWithoutSpaces = "";
			for (int i = 0; i < expression.Length; i++)
			{
				if (expression[i].Equals(' '))
				{
					continue;
				}
				else
				{
					expressionWithoutSpaces += expression[i];
				}
			}
			return expressionWithoutSpaces;
		}

		/// <summary>
		/// Returns true, if char is operator and false in another case 
		/// </summary>
		/// <param name="с"></param>
		/// <returns></returns>
		static public bool IsOperator(char с)
		{
			if (("+-/*".IndexOf(с) != -1))
				return true;
			return false;
		}

		/// <summary>
		/// Returns operator priority
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		static public byte GetPriority(char s)
		{
			switch (s)
			{
				case '+': 
					return 1;
				case '-': 
					return 2;
				case '*': 
					return 3;
				case '/': 
					return 4;
				default: 
					return 5;
			}
		}

		/// <summary>
		/// Gets expression and returns it in postfix notation
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		static public string GetPostfixExpression(string input)
		{
			string output = "";
			Stack<char> operStack = new Stack<char>();

			for (int i = 0; i < input.Length; i++)
			{
				if (Char.IsDigit(input[i])) 
				{
					while (!IsOperator(input[i]))
					{
						output += input[i];
						i++;

						if (i == input.Length) break;
					}

					output += " ";
					i--;
				}

				if (IsOperator(input[i]))
				{

					if (operStack.Count > 0)
						if (GetPriority(input[i]) <= GetPriority(operStack.Peek()))
							output += operStack.Pop().ToString() + " ";

					operStack.Push(char.Parse(input[i].ToString()));
				}
			}
			while (operStack.Count > 0)
				output += operStack.Pop() + " ";

			return output;
		}

		/// <summary>
		/// Counts expression in postfix notation and returns the result
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		static public int Counting(string input)
		{
			int result = 0;
			Stack<int> tmp = new Stack<int>();

			for (int i = 0; i < input.Length; i++)
			{
				if (Char.IsDigit(input[i]))
				{
					string a = "";

					while (input[i] != ' ' && !IsOperator(input[i]))
					{
						a += input[i];	
						i++;
						if (i == input.Length) break;
					}
					tmp.Push(int.Parse(a));
					i--;
				}
				else if (IsOperator(input[i]))
				{
					int a = tmp.Pop();
					int b = tmp.Pop();

					switch (input[i])
					{
						case '+': 
							result = b + a; break;
						case '-': 
							result = b - a; break;
						case '*': 
							result = b * a; break;
						case '/': 
							result = b / a; break;
					}
					tmp.Push(result);
				}
			}
			return tmp.Peek();
		}

		/// <summary>
		/// Converts expression with roman digits to expression with integers
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		public static string ToNormalNumbers(string[] expression) {
			string result = Convert.ToString(ToInt(expression[0].ToCharArray()));
			for (int i = 1; i < expression.Length - 1; i += 2)
			{
				char graphicSymbol;
				graphicSymbol = expression[i].ToCharArray()[0];
				if (ToInt(expression[i + 1].ToCharArray()) == 0) {
					throw new Exception("Expression is not correct!");
				}
				switch (graphicSymbol)
				{
					case '+':
						result += "+" + ToInt(expression[i + 1].ToCharArray());
						break;
					case '-':
						result += "-" + ToInt(expression[i + 1].ToCharArray());
						break;
					case '*':
						result += "*" + ToInt(expression[i + 1].ToCharArray());
						break;
					case '/':
						result += "/" + ToInt(expression[i + 1].ToCharArray());
						break;
				}
			}
			return result;
		}
	}
}

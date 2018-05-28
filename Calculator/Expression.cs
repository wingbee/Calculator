using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
	public class Expression : ExpressionBase
	{		
		//Characteristics
		public Expression(string expressionString) : base(expressionString)
		{
		}

		public virtual double Evaluate()
		{
			try
			{
				return Evaluate(string.Copy(ExpressionString));
			}
			catch (Exception ex)
			{
				throw new Exception($"Unable to evaluate expression: {ExpressionString}", ex);
			}
		}

		private double Evaluate(string unparsedExpressionString)
		{
			double result = 0;
			
			Term term = ParseLeftmostTerm(ref unparsedExpressionString);
			result = term.Evaluate();

			while (!string.IsNullOrEmpty(unparsedExpressionString))
			{
				Operation operation = ParseLeftmostOperator(unparsedExpressionString);
				term = ParseLeftmostTerm(ref unparsedExpressionString);

				if (operation == Operation.PLUS)
				{
					result += term.Evaluate();
				}
				else
				{
					result -= term.Evaluate();
				}
			}

			return result;
		}
		
		private Term ParseLeftmostTerm(ref string unparsedExpressionString)
		{
			Term term = null;
			
			//Search for first term
			int index = 1;
			while (index < unparsedExpressionString.Length)
			{				
				string searchString = unparsedExpressionString.Substring(0, index + 1);

				if (IsTermSeperator(searchString, index))
				{		
					term = 
						new Term(
							unparsedExpressionString.Substring(0, index));

					unparsedExpressionString =
						unparsedExpressionString.Skip(index).ToString();

					return term;
				}

				index++;
			}

			//No term seperator found, expressionString contains single term
			term = new Term(unparsedExpressionString);
			unparsedExpressionString = string.Empty;
			return term;
		}

		private bool IsTermSeperator(string searchString, int index)
		{
			//A valid term seperator must satisfy the following rules:
			//   - must a '+' or '-'
			//   - must be on bracket level 0, i.e. not nested inside one or more levels of bracket
			//   - must be preceded by a ')' or a digit
			char currentChar = searchString[index];
			char previousChar = searchString[index - 1];

			return
				IsTermSeparatorSymbol(currentChar) &&
				GetBracketLevel(searchString) == 0 &&
				(
					previousChar == BRACKET_CLOSE ||
					IsDigit(previousChar)
				);
		}

		private int GetBracketLevel(string searchString)
		{
			return
				searchString.Count(s => s == BRACKET_OPEN) -
				searchString.Count(s => s == BRACKET_CLOSE);
		}
			    
		private Operation ParseLeftmostOperator(string unparsedExpressionString)
		{
			char potentialOperator = unparsedExpressionString[0];

			if (IsTermSeparatorSymbol(potentialOperator))
			{
				Operation operation = 
					(Operation)Enum.Parse(
						typeof(Operation), 
					potentialOperator.ToString());

				unparsedExpressionString.Skip(1).ToString();
				
				return operation;
			}
			else
			{
				throw new Exception($"Plus or minus operator excepted, found: {potentialOperator}");
			}
		}
	}
}

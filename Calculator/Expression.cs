using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
	public class Expression : ExpressionBase
	{
		private List<Term> _terms;
		private List<Operator> _termSeparators;
		
		//Characteristics
		public Expression(string expressionString) : base(expressionString)
		{
		}

		public virtual double Evaluate()
		{
			try
			{
				_terms = new List<Term>();
				_termSeparators = new List<Operator>();
				return Evaluate(string.Copy(ExpressionString));
			}
			catch (Exception ex)
			{
				throw new Exception($"Unable to evaluate expression: {ExpressionString}", ex);
			}
		}

		private double Evaluate(string unparsedExpressionString)
		{
			ParseTermsAndTermSeparators(unparsedExpressionString);

			if (_terms.Count == 1)
			{
				return EvaluateTerm();
			}
			else
			{
				return EvaluateTerms();
			}
		}

		private void ParseTermsAndTermSeparators(string unparsedExpressionString)
		{
			unparsedExpressionString = ParseLeftmostTerm(unparsedExpressionString);

			if (!string.IsNullOrEmpty(unparsedExpressionString))
			{
				unparsedExpressionString = ParseTermSeparatorOperation(unparsedExpressionString);
				ParseTermsAndTermSeparators(unparsedExpressionString);
			}
		}
		
		private string ParseLeftmostTerm(string unparsedExpressionString)
		{
			//Search for first term
			int index = 1;
			while (index < unparsedExpressionString.Length)
			{				
				string searchString = unparsedExpressionString.Substring(0, index + 1);

				if (IsTermSeperator(searchString, index))
				{
					_terms.Add(
						new Term(
							unparsedExpressionString.Substring(0, index)));

					return unparsedExpressionString.Skip(index).ToString();
				}

				index++;
			}

			//No term seperator found, expressionString contains single term
			_terms.Add(new Term(unparsedExpressionString));			
			return string.Empty;
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
			    
		private string ParseTermSeparatorOperation(string unparsedExpressionString)
		{
			char potentialOperator = unparsedExpressionString[0];

			if (IsTermSeparatorSymbol(potentialOperator))
			{
				_termSeparators.Add(
					(Operator)Enum.Parse(
						typeof(Operator), 
						potentialOperator.ToString()));
			}
			else
			{
				throw new Exception($"Plus or minus operator excepted, found: {potentialOperator}");
			}

			return unparsedExpressionString.Skip(1).ToString();
		}

		private double EvaluateTerm()
		{
			return _terms.First().Evaluate();
		}
		
		private double EvaluateTerms()
		{
			double result = _terms.First().Evaluate();

			for (int i = 0; i < _termSeparators.Count; i++)
			{
				double termValue = _terms[i + 1].Evaluate();
				
				if (_termSeparators[i] == Operator.PLUS)
				{
					result += termValue;
				}
				else
				{
					result -= termValue;
				}
			}

			return result;
		}
	}
}

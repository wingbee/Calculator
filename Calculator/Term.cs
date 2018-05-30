using System;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace Calculator
{
	public class Term : Expression
	{	
		public Term(string expressionString) : base (expressionString)
		{
		}

		public override double Evaluate()
		{
			if (IsBracketExpression(ExpressionString))
			{	
				return new BracketExpression(ExpressionString).Evaluate();
			}
			else
			{
				return Evaluate(string.Copy(ExpressionString));
			}
		}

		private bool IsBracketExpression(string expressionString)
		{
			return
				expressionString.First() == BRACKET_OPEN &&
				expressionString.Last() == BRACKET_CLOSE;
		}
		
		private double Evaluate(string unparsedExpressionString)
		{
			double result = 0;

			Factor factor = ParseLeftmostFactor(ref unparsedExpressionString);
			result = factor.Evaluate();

			while (!string.IsNullOrEmpty(unparsedExpressionString))
			{
				Operation operation = ParseLeftmostOperator(unparsedExpressionString);
			    factor = ParseLeftmostFactor(ref unparsedExpressionString);

				if (operation == Operation.MULTIPLY)
				{
					result += factor.Evaluate();
				}
				else
				{
					result /= factor.Evaluate();
				}
			}

			return result;
		}

		private Factor ParseLeftmostFactor(ref string unparsedExpressionString)
		{
		    Factor factor = null;

		    char leftMostChar = unparsedExpressionString[0];

		    if (leftMostChar == MINUS)
		    {
		        return ParseLeftMostMinusSign(ref unparsedExpressionString);
		    }
		    else if (leftMostChar == BRACKET_OPEN)
		    {
		        return ParseLeftmostBracketedFactor(ref unparsedExpressionString);
		    }
		    else if (IsDigit(leftMostChar))
		    {
		        return ParseLeftmostNumber(ref unparsedExpressionString);
		    }
		    else
		    {
		        throw new Exception($"Unable to parse factor beginning with {leftMostChar}");
		    }
        }

	    private Factor ParseLeftMostMinusSign(ref string unparsedExpressionString)
	    {	        
	        Factor factor = new Factor(MINUS.ToString());

	        unparsedExpressionString = 
	            unparsedExpressionString.Skip(1).ToString();

	        return factor;
	    }

	    private Factor ParseLeftmostBracketedFactor(ref string unparsedExpressionString)
	    {
	        int indexBracketClose =
	            unparsedExpressionString.IndexOf(
	                BRACKET_CLOSE.ToString());

            Factor factor = 
                new Factor(
                    unparsedExpressionString.Substring(0, indexBracketClose + 1));

	        if (indexBracketClose == unparsedExpressionString.Length - 1)
	        {
	            unparsedExpressionString = string.Empty;
	        }
	        else
	        {
	            unparsedExpressionString = 
                    unparsedExpressionString.Substring(indexBracketClose + 1);
            }

	        return factor;
	    }

	    private Factor ParseLeftmostNumber(ref string unparsedExpressionString)
	    {
	        int index = 0;
	        bool endOfNumberFound = false;
	        int lengthOfNumber = unparsedExpressionString.Length;

	        while (!endOfNumberFound && index < unparsedExpressionString.Length)
	        {

	        }


	    }

		private Operation ParseLeftmostOperator(string unparsedExpressionString)
		{
			char potentialOperator = unparsedExpressionString[0];

			if (IsFactorSeparatorSymbol(potentialOperator))
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
				//Implicit multiplication
			    return Operation.MULTIPLY;
			}
		}
	}
}

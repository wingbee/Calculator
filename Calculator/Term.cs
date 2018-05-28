using System;
using System.Linq;

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
		
		private double Evaluate(string unevaluatedExpressionString)
		{
			return double.Parse(unevaluatedExpressionString);
		}
	}
}

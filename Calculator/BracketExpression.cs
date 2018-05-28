using System;
namespace Calculator
{
	public class BracketExpression : Expression
	{
		public BracketExpression(string expressionString) : base(expressionString)
		{
		}

		public override double Evaluate()
		{
			string expressionInsideBrackets =
				GetExpressionInsideBrackets(
					string.Copy(ExpressionString));
			
			return 
				new Expression(expressionInsideBrackets).Evaluate();
		}
		
		private string GetExpressionInsideBrackets(string expressionString)
		{
			//Remove "(" at front
			expressionString = expressionString.Substring(1);  
			
			//Remove ")" at end
			expressionString = 
				expressionString.Remove(
					expressionString.Length - 1);   

			return expressionString;
		}
	}
}

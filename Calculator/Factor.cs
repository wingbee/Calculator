using System;
namespace Calculator
{
	public class Factor : Expression
	{
		public Factor(string expressionString) : base(expressionString)
		{
		}

	    public override double Evaluate()
	    {
	        if (ExpressionString == MINUS.ToString())
	        {
	            return -1;
	        }
	    }
	}
}

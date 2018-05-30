using System;

namespace Calculator
{
	public class ExpressionBase
	{
		public const char PLUS = '+';
		public const char MINUS = '-';
		public const char MULTIPLY = '*';
		public const char DIVIDE = '/';

		public const string SPACE = " ";

		public const char BRACKET_OPEN = '(';
		public const char BRACKET_CLOSE = ')';

	    public const char DECIMAL_POINT = '.';

        public virtual string ExpressionString { get; private set; }
		
		public ExpressionBase(string expressionString)
		{
			if (string.IsNullOrWhiteSpace(expressionString))
			{
				throw new Exception("Expression is null or whitespace");
			}
			
			ExpressionString = GetWhitespacelessString(expressionString);
		}
		
		protected string GetWhitespacelessString(string expressionString)
		{
			return string.Copy(expressionString).Replace(SPACE, string.Empty);
		}
		
		protected bool IsTermSeparatorSymbol(char character)
		{
			return character == PLUS || character == MINUS;
		}
		
		protected bool IsFactorSeparatorSymbol(char character)
		{
			return character == MULTIPLY || character == DIVIDE;
		}

		protected bool IsDigit(char character)
		{
			return Char.IsDigit(character);
		}
	}
}

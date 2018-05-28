using System;
using System.Collections.Generic;

namespace Calculator
{
	public class CalculatorEngine
	{
		private Dictionary<string, int> _variables;
		
		public CalculatorEngine()
		{
			_variables = new Dictionary<string, int>();	
		}

		public double PerformAssignment(string assignmentString)
		{
			string[] assignmentStringParts = assignmentString.Split('=');
			
			string variableName = assignmentStringParts[0].Replace(" ", "");
			string expressionString = assignmentStringParts[1];

			return EvaluateExpression(expressionString);
		}
		
		public double EvaluateExpression(string expressionString)
		{
			return new Expression(expressionString).Evaluate();
		}
	}
}

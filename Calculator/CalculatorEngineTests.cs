using System;
using NUnit.Framework;

namespace Calculator
{
	[TestFixture]
	public class CalculatorEngineTests
	{
		private CalculatorEngine _calculatorEngine;
		
		[SetUp]
		public void Setup()
		{
			_calculatorEngine = new CalculatorEngine();
		}
		
		[Test]
		public void Tests()
		{
			//AssertEvaluation(1, 	"1");
			//AssertEvaluation(-1, 	"-1");
			//AssertEvaluation(1, 	"(1)");
			//AssertEvaluation(1, 	"((1))");
			//AssertEvaluation(-1, 	"(-1)");
			AssertEvaluation(-1, 	"-(1)");
			//AssertEvaluation(1, 	"-(-1)");
			//AssertEvaluation(-1, 	"((-1))");
			//AssertEvaluation(1, 	"-((-1))");
			//AssertEvaluation(-1, 	"-(-(-1))");
			//AssertEvaluation(-1, 	"-(-((-1)))");
			//AssertEvaluation(45, 	"-(3 + (5 * -8)) - -10 + (-1 - -2) * -2");
			
		}

		private void AssertEvaluation(double expectedValue, string expressionString)
		{
			Assert.AreEqual(expectedValue, _calculatorEngine.EvaluateExpression(expressionString));
		}
	}
	
	/*
	 	1*6                 1*6
		1*-9-10             1*-9  -   10
		-1-6                -1    -   6
		--3-12              --3   -   12
		1*-9--10            1*-9  -   -10 
		1*-9-(-10)          1*-9  -   (-10)
		1*-9-(-(1))-10      1*-9  -   (-(1))  10 
		1*-9-((-(1))-10)    1*-9  -   ((-(1))-10)
	 */ 
}

using System;
using System.Collections.Generic;

namespace Calculator
{
	public class CalculatorConsole
	{
		private CalculatorEngine _calculatorEngine;
		private List<string> _inputs;
		private List<string> _outputs;
		
		private bool _endProgram;
		
		public CalculatorConsole()
		{
			_calculatorEngine = new CalculatorEngine();
			_inputs = new List<string>();
			_outputs = new List<string>();
			_endProgram = false;
		}

		public void Run()
		{
			while (!_endProgram)
			{
				string input = AskInput();
				_inputs.Add(input);
				string output = ProcessInput(input);
				_outputs.Add(output);
			}
		}

		private string AskInput()
		{
			Console.WriteLine("Enter expression");
			return Console.ReadLine();
		}

		private string ProcessInput(string input)
		{	
			string output;
			
			string inputInUpperCaseWithoutSpaces =
				input.ToUpper().Replace(" ", "");

			switch (inputInUpperCaseWithoutSpaces)
			{
				case "QUIT":
					output = QuitProgram();
					break;
				case "HISTORY":
					output = GetHistory();
					break;
				default:
					output = EvaluateExpression(input);
					break;
			}

			return output;
		}

		private string QuitProgram()
		{
			_endProgram = true;
			
			return "Quitting Program";
		}
		
		private string GetHistory()
		{
			string result = "";
			
			int numberOfInputs = _inputs.Count;

			//Don't print the most recent ("HISTORY"), input			
			for (int i = 0; i < numberOfInputs - 1; i++)
			{
				result += 
					_inputs[i] + " = " + _outputs[i] + "\n";
			}

			return result;
		}
		
		private string EvaluateExpression(string input)
		{
			return 
				_calculatorEngine
					.EvaluateExpression(input)
					.ToString();
		}
	}
}

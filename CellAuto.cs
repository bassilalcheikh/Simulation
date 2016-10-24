/*
	1-D Cellular Automaton Program
	By: Bassil Alcheikh

	This C#/.NET program simulates 1-dimensional cellular automaton.
	Credit to Daniel Shiffman, author of "The Nature of Code," for providing insight on
	how to code a CA program using object oriented programming.
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading;

public class CellAuto{
	public static void Main(){ // Main Method

		CellAutoClass.Greeting();

		string user_input = System.IO.File.ReadAllText(@"CA_input_file.txt"); // input initial vector (with commas)
		int[] userinput_vector = user_input.Split(',').Select(n => Convert.ToInt32(n)).ToArray(); // convert string vector to int array

		Console.WriteLine("Enter desired iterations: ");
		int iteration = Convert.ToInt32(Console.ReadLine()); // prompt user for number of iterations
		Console.WriteLine("--------------------");

		CellAutoClass.PrintCells(userinput_vector); //display first set of binary cubes

		int[] new_state_vector = new int[userinput_vector.Length]; // instantiate new array for loop

		for(int k=0; k < iteration; k++){ // loop runs the "rule" for each set of values
			new_state_vector = CellAutoClass.NewState(userinput_vector);
			//Thread.Sleep(45);   //<- OPTIONAL: time delay between each iteration
			CellAutoClass.PrintCells(new_state_vector);
			userinput_vector=new_state_vector;
		}
		Console.WriteLine("--------------------");
	}
}

public class CellAutoClass{

	public static void Greeting(){	// Greeting message
		Console.WriteLine("===============================================");
		Console.WriteLine("Welcome to the Cellular Automaton Program.");
		//Console.WriteLine("Please enter the input vector, in 0's and 1's: ");
	}

	public static void PrintCells(int[] input_vector){	// Prints cells via to binary code

		for (int i=0; i<input_vector.Length; i++){

			if (input_vector[i]==0){
				Console.Write(" ️");
			}
			else {
				Console.Write("■");
			}
		}
		Console.WriteLine(" ");
	}

	public static int CellRule(int[] input_vec){	// Rule for each cell

		double input_to_dec = 0;

		for (int i = 0; i < 3; i++){
			double val = Convert.ToDouble(input_vec[i]);
   			input_to_dec = input_to_dec+(val*Math.Pow(2,(2-i)));
		}

		int new_value=0;

		int switch_input = Convert.ToInt32(input_to_dec);

		switch (switch_input){

			case 7: // 111
				new_value = 0;
				break;

    		case 6: // 110
    			new_value = 1;
				break;

    		case 5: // 101
    			new_value = 0;
				break;

	    	case 4: // 100
    			new_value = 1;
				break;

	    	case 3: // 011
    			new_value = 1;
				break;

	   		case 2: // 010
   				new_value = 0;
				break;

	   		case 1: // 001
   				new_value = 1;
				break;

	   		case 0: // 000
   				new_value = 0;
				break;
   		}
   		return new_value;
	}
	// Generates new binary states. Input: char array. Output: char array.
	public static int[] NewState(int[] input_vector){

		int length = input_vector.Length;
		int[] new_vector = new int[length];

		for (int i = 1; i < length-1; i++) {
  			int[] analyze_vector = new int[3];
  			analyze_vector[0] = input_vector[i-1];
  			analyze_vector[1] = input_vector[i];
  			analyze_vector[2] = input_vector[i+1];

  			new_vector[i]=CellRule(analyze_vector);
		}

		new_vector[0]=input_vector[0];
		new_vector[length-1]=input_vector[length-1];

		return new_vector;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace WeightedRandom
{
	/// <summary>
	/// A simple example of how to use WeightedRandom.
	/// </summary>
	public class WeightedRandomDemo
	{

		public static void main(string[] args)
		{

			// Create the new WeightedRandom with the type of Object you'd like to select from.
			WeightedRandom<int> random = new WeightedRandom<int> ();

			// Use the Add() method to populate the WeightedRandom. Here the ints in the first 
			// argument are being added with the weight of the second argument.
			random.Add (50, 50);
			random.Add (20, 20);
			random.Add (15, 15);
			random.Add (16, 15);
			//random.Add(item, weight);

			// These are being used to keep track of how many times each item has been randomly
			// selected.
			int numOf50 = 0;
			int numOf20 = 0;
			int numOf15 = 0;
			int numOf16 = 0;

			// The number of times a random item will be drawn. In this example, The larger
			// this number is, the closer the resulting percentages will be to the number
			// next to it.
			int numOfTimes = 1000000;

			for (int i = 0; i < numOfTimes; i++) 
			{

				// The call to Random() generates the random item.
				int temp = random.Next ();

				// Checks the random item and logs it for the calculations below.
				switch (temp) 
				{
					case 50: 
						numOf50++; 
						break;
					case 20:
						numOf20++;
						break;
					case 15:
						numOf15++;
						break;
					case 16:
						numOf16++;
						break;
					default:
						break;
				}
			}

			// Prints the percentage of times an item was selected throughout the loop.
			Console.Write ("Number | Percentage");
			Console.Write ("50     | " + (((double)numOf50 / (double)numOfTimes) * 100) + "%");
			Console.Write ("20     | " + (((double)numOf20 / (double)numOfTimes) * 100) + "%");
			Console.Write ("15 #1  | " + (((double)numOf15 / (double)numOfTimes) * 100) + "%");
			Console.Write ("15 #2  | " + (((double)numOf16 / (double)numOfTimes) * 100) + "%");
		}
	}
}
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

namespace WeightedRandom
{
	/// <summary>
	/// Stores a collection of items with specified weights. These weights will allow for sudo-randomness, with each item
	/// being selected at the percentage determined by the weight of the item divided by the total weight of all the items.
	/// </summary>
	/// See <see cref="WeightedRandomDemo"/> for an example of how to use this class.
	public class WeightedRandom<T> 
	{
	    private SortedDictionary<int, LinkedList<T>> weightedItems;
		private System.Random random;
		private int totalWeight;
	    
		/// <summary>
		/// Initializes a new instance of the <see cref="WeightedRandom`1"/> class.
		/// </summary>
		/// <remarks>
		/// This object cannot be used unless it is populated. This can be done using
		/// the <c>Add()</c> method.
		/// </remarks>
	    public WeightedRandom() 
		{
			weightedItems = new SortedDictionary<int, LinkedList<T>>();
			random = new System.Random ();
			totalWeight = 0;
	    }

		/// <summary>
		/// Add the specified item with the specified weight.
		/// </summary>
		/// <param name="item">The item to be added to the list.</param>
		/// <param name="weight">The weight to use with the added object.</param>
		/// <exception cref="ArgumentException">Thrown if the given weight is negative.</exception>
	    public void Add(T item, int weight) 
		{
			if (weight < 0)
				throw new ArgumentException ("Weights cannot be negative.");
	        if(!weightedItems.ContainsKey(weight)) 
	            weightedItems.Add(weight, new LinkedList<T>());
	        weightedItems[weight].AddFirst(item);
			totalWeight += weight;
	    }

		/// <summary>
		/// Add the specified item with the specified weight.
		/// </summary>
		/// <param name="item">The item to be added to the list.</param>
		/// <param name="weight">The weight to use with the added object. Weights are always rounded down and calculated
		/// as integers.
		/// </param>
		/// <see cref="Add(T, int)"/> 
	    public void Add(T item, float weight) 
		{
			this.Add(item, (int)Math.Floor(weight));
	    }

		/// <summary>
		/// Add the specified item with the specified weight.
		/// </summary>
		/// <param name="item">The item to be added to the list.</param>
		/// <param name="weight">The weight to use with the added object. Weights are always rounded down and calculated
		/// as integers.
		/// </param>
		/// <see cref="Add(T, int)"/> 
	    public void Add(T item, double weight) 
		{
			this.Add(item, (int)Math.Floor(weight));
	    }

		/// <summary>
		/// Add the specified items with the specified weights.
		/// </summary>
		/// <param name="items">The items to be added to the list.</param>
		/// <param name="weights">The weights of the items to be added to the list.</param>
		/// <exception cref="ArgumentException">Thrown if the given arrays aren't of equal length.</exception>
		public void Add(T[] items, int[] weights) {
			if (items.Length != weights.Length)
				throw new ArgumentException ("Arrays must be equal lengths.");
			for(int i = 0; i < items.Length; i++) {
				this.Add (items [i], weights [i]);
			}
		}

		/// <summary>
		/// Add the specified items with the specified weights.
		/// </summary>
		/// <param name="items">The items to be added to the list.</param>
		/// <param name="weights">The weights of the items to be added to the list. Weights are always rounded down and
		/// calculated as integers.
		/// </param>
		/// <exception cref="ArgumentException">Thrown if the given arrays aren't of equal length.</exception>
		public void Add(T[] items, float[] weights) {
			if (items.Length != weights.Length)
				throw new ArgumentException ("Arrays must be equal lengths.");
			for(int i = 0; i < items.Length; i++) {
				this.Add (items [i], weights [i]);
			}
		}

		/// <summary>
		/// Add the specified items with the correlating weights.
		/// </summary>
		/// <param name="items">The items to be added to the list.</param>
		/// <param name="weights">The weights of the items to be added to the list. Weights are always rounded down and
		/// calculated as integers.
		/// </param>
		/// <exception cref="ArgumentException">Thrown if the given arrays aren't of equal length.</exception>
		public void Add(T[] items, double[] weights) {
			if (items.Length != weights.Length)
				throw new ArgumentException ("Given arrays must be equal lengths.");
			for(int i = 0; i < items.Length; i++) {
				this.Add (items [i], weights [i]);
			}
		}

		/// <summary>
		/// Removes the specified item that has the specified weight.
		/// </summary>
		/// <param name="item">The item to be removed from the WeightedRandom Object.</param>
		/// <param name="weight">The weight of the item to be removed from the WeightedRandom Object.</param>
		/// <exception cref="ArgumentException">Thrown when there are no items witht the specified weight, or if 
		/// there are no instances of the specified item with the speicified weight.</exception>
		public void Remove(T item, int weight)
		{
			if (!weightedItems.ContainsKey (weight))
				throw new ArgumentException ("There are no items with a weight of " + weight);
			if (!weightedItems [weight].Contains (item))
				throw new ArgumentException ("There is no " + item + "with a weight of " + weight);

			weightedItems [weight].Remove (item);
		}

		/// <summary>
		/// Removes every instance of the specified item from the WeightedRandom Object.
		/// </summary>
		/// <param name="item">The item to remove from the WeightedRandom Object.</param>
	    public void Remove(T item) 
		{
			foreach(int weight in weightedItems.Keys) 
			{
				if (weightedItems [weight].Contains (item))
					weightedItems [weight].Remove (item);
				if (weightedItems [weight].Count == 0)
					this.Remove (weight);
	        }
	    }

		/// <summary>
		/// Removes every item with the specified weight.
		/// </summary>
		/// <param name="weight">The weight to remove all the items from.</param>
	    public void Remove(int weight) 
		{
	        weightedItems.Remove(weight);
	    }

		/// <summary>
		/// Returns the percentage of times that the given item will statistically be returned by the <c>Random()</c>
		/// method.
		/// </summary>
		/// <returns>The percentage of time that the given item will statistically be return by the <c>Random()</c>
		/// method.
		/// </returns>
		/// <param name="item">The item to be checked.</param>
		public double GetPercentage(T item)
		{
			int[] weights = weightedItems.Keys.ToArray();

			foreach(int weight in weights)
			{
				if (weightedItems [weight].Contains (item))
					return ((double)weight / (double)totalWeight);		
			}
			throw new ArgumentException("The specified item could not be found.");
		}

		/// <summary>
		/// Returns a random item. This randomness is determined by the weight of each item.
		/// </summary>
		/// <exception cref="WeightedRandomException">Thrown if the WeightedRandom object is empty. Use the <c>Add()</c>
		/// method to populate it.</exception>
	    public T Random() {
	        if(weightedItems.Count == 0) {
				throw new WeightedRandomException ("There are no objects in the list! \n"
					+ "Use the Add() method to populate the WeightedRandom object.");
	        }
				
	        int randomNum = random.Next(0, totalWeight + 1);
			int[] weights = weightedItems.Keys.Reverse().ToArray();

			for (int i = 0; i < weights.Length - 1; i++) 
			{
				int weight = weights [i];
				if ((randomNum -= weight) < 0) 
				{
					int itemsWithWeight = weightedItems [weight].Count;
					if (itemsWithWeight > 1)
						return ResolveDuplicateWeight (weight, itemsWithWeight);
					return weightedItems [weight].ElementAt(0);
				}
			}
			return ResolveFinalWeight (weights);
	    }

		/// <summary>
		/// Resolves the final weight.
		/// </summary>
		/// <returns>the item that should be returned from the lowest weight.</returns>
		/// <param name="weights">The array of weights currently stored by the WeightedRandom.</param>
		private T ResolveFinalWeight(int[] weights) 
		{
			int lastWeight = weights [weights.Length - 1];
			int itemsWithWeight = weightedItems [lastWeight].Count;

			if (itemsWithWeight > 1)
				return ResolveDuplicateWeight (lastWeight, itemsWithWeight);
			return weightedItems [lastWeight].ElementAt(0);
		}

		/// <summary>
		/// Returns a random item with the specified weight.
		/// </summary>
		/// <returns>A random item with the specified weight.</returns>
		/// <param name="weight">The weight of the item to be returned.</param>
		/// <param name="itemsWithWeight">The number of items currently stored with the speicified weight.</param>
		private T ResolveDuplicateWeight(int weight, int itemsWithWeight) 
		{
			int randomNum = random.Next (0, itemsWithWeight);
			return weightedItems [weight].ElementAt(randomNum);
		}
	}
}
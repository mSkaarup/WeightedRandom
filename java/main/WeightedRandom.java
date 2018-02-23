package weightedrandom.main;

import java.util.Collections;
import java.util.LinkedList;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import java.util.concurrent.ThreadLocalRandom;

/**
 * Stores a collection of items with specified weights. These weights will allow
 * for sudo-randomness, with each item being selected at the percentage 
 * determined by the weight of the item divided by the total weight of all the 
 * items.
 * 
 * @author Marc Skaarup
 * @param <T> The type of Object to be randomly selected from.
 */
public class WeightedRandom<T> {

    private final Map<Integer, List<T>> weightedItems;
    private int totalWeight;
    
    /**
     * Creates an empty WeightedRandom Object. Must use the add() method to add
     * a weighted item, which can then be randomly selected from.
     */
    public WeightedRandom() {
        weightedItems = new TreeMap<>(Collections.reverseOrder());
        totalWeight = 0;
    }
    
    /**
     * Adds a weighted item to be randomly selected from.
     * 
     * @param item - The item that can be randomly selected.
     * @param weight - The weight to be associated with this given item. Weights
     * are always calculated as integers. Cannot be negative.
     * @throws IllegalArgumentException if the given weight is negative.
     */
    public void add(T item, int weight) throws IllegalArgumentException {
        if(weight < 0) {
            throw new IllegalArgumentException("Weights cannot be negative!");
        }
        if(!weightedItems.containsKey(weight)) {
            weightedItems.put(weight, new LinkedList<>());
        }
        weightedItems.get(weight).add(item);
        totalWeight += weight;
    }
    
    /**
     * Adds a weighted item to be randomly selected from.
     * 
     * @param item The item that can be randomly selected.
     * @param weight The weight to be associated with this given item. Weights
     * are always rounded down and calculated as integers. Cannot be negative.
     */
    public void add(T item, double weight) {
        this.add(item, (int)Math.floor(weight));
    }
    
    /**
     * Adds a weighted item to be randomly selected from.
     * 
     * @param item The item that can be randomly selected.
     * @param weight The weight to be associated with this given item. Weights
     * are always rounded down and calculated as integers. Cannot be negative.
     */
    public void add(T item, float weight) {
        this.add(item, (int)Math.floor(weight));
    }
    
    /**
     * Adds the specified items with the correlating weights.
     * 
     * @param items The items to be added to the list.
     * @param weights The weights of the items to be added to the list.
     * @throws IllegalArgumentException if the given arrays aren't of equal
     * length.
     */
    public void add(T[] items, int[] weights) throws IllegalArgumentException {
        if(items.length != weights.length)
            throw new IllegalArgumentException("Given arrays must be equal"
                    + "lengths.");
        for(int i = 0; i < items.length; i++) {
            this.add(items[i], weights[i]);
        }
    }
    
    /**
     * Adds the specified items with the correlating weights.
     * 
     * @param items The items to be added to the list.
     * @param weights The weights of the items to be added to the list Weights
     * are always rounded down and calculated as integers.
     * @throws IllegalArgumentException if the given arrays aren't of equal
     * length.
     */
    public void add(T[] items, double[] weights) throws IllegalArgumentException {
        if(items.length != weights.length)
            throw new IllegalArgumentException("Given arrays must be equal"
                    + "lengths.");
        for(int i = 0; i < items.length; i++) {
            this.add(items[i], weights[i]);
        }
    }
    
    /**
     * Adds the specified items with the correlating weights.
     * 
     * @param items The items to be added to the list.
     * @param weights The weights of the items to be added to the list. Weights
     * are always rounded down and calculated as integers.
     * @throws IllegalArgumentException if the given arrays aren't of equal
     * length.
     */
    public void add(T[] items, float[] weights) throws IllegalArgumentException {
        if(items.length != weights.length)
            throw new IllegalArgumentException("Given arrays must be equal"
                    + "lengths.");
        for(int i = 0; i < items.length; i++) {
            this.add(items[i], weights[i]);
        }
    }
    
    /**
     * Removes the given item that has the specified weight.
     * 
     * @param item The item to be removed from the WeightedRandom Object.
     * @param weight The weight of the item to be removed from the 
     * WeightedRandom Object.
     * @throws IllegalArgumentException if there are no items with the given
     * weight, or if there is no instance of the item with the given weight.
     */
    public void remove(T item, int weight) throws IllegalArgumentException {
        if(!weightedItems.containsKey(weight)) {
            throw new IllegalArgumentException("There are no items with a "
                    + "weight of " + weight);
        }
        if(!weightedItems.get(weight).contains(item)) {
            throw new IllegalArgumentException("There is no " + item + "with a "
                    + "weight of " + weight);
        }
        weightedItems.get(weight).remove(item);
        totalWeight -= weight;
    }
    
    /**
     * Removes the given item from the list. 
     * 
     * @param item The item to be removed from the list.
     */
    public void remove(T item) {
        for(Integer i : weightedItems.keySet()) {
            if(weightedItems.get(i).contains(item)) {
                weightedItems.get(i).remove(item);
                totalWeight -= i;
                if(weightedItems.get(i).isEmpty()) {
                    this.remove(i);
                }
            }
        }
    }
    
    /**
     * Removes all items with the given weight.
     * 
     * @param weight The weight of items to be removed from the list.
     */
    public void remove(int weight) {
        weightedItems.remove(weight);
        totalWeight -= (weight * weightedItems.get(weight).size());
    }
    
    /**
     * Returns the percentage of times that the given item will statistically be
     * returned by the random() method.
     * 
     * @param item The item to be checked.
     * @return The percentage of times that the given item will statistically be
     * returned by the random() method.
     */
    public double getPercentage(T item) {
        for(Integer i : weightedItems.keySet()) {
            if(weightedItems.get(i).contains(item))
                return ((double)i / (double)totalWeight); 
        }
        throw new IllegalArgumentException("The specified item could "
                        + "not be found.");
    }
    
    /**
     * Returns a random object from the items currently held by the 
     * WeightedRandom object. 
     * 
     * @return A random object from the items currently held by the 
     * WeightedRandom object.
     * @throws WeightedRandomException if there aren't any items in the list.
     */
    public T next() throws WeightedRandomException {
        if(weightedItems.isEmpty()) {
            throw new WeightedRandomException("There are no items in the list! \n"
                    + "Use the add() method to populate the WeightedRandom object.");
        }
        
        int randomNum = ThreadLocalRandom.current().nextInt(0, totalWeight + 1);
        Integer[] weights = new Integer[weightedItems.size()];
        weightedItems.keySet().toArray(weights);
        
        for(int i = 0; i < weights.length - 1; i++) {
            Integer weight = weights[i];
            if((randomNum -= weight) < 0) {
                int itemsWithWeight = weightedItems.get(weight).size();
                if(itemsWithWeight > 1) {
                   return resolveDuplicateWeight(weight, itemsWithWeight);
                }
                return weightedItems.get(weight).get(0);
            }
        }
        return determineFinalWeight(weights);
    }
    
    /**
     * If all the other items weren't selected, chooses an item from the
     * smallest weight.
     * 
     * @param weights The set of weights that can be selected from.
     * @return The object mapped to the given weight.
     */
    private T determineFinalWeight(Integer[] weights) {
        Integer lastWeight = weights[weights.length - 1];
        int itemsWithWeight = weightedItems.get(lastWeight).size();
        if(itemsWithWeight > 1) {
            return resolveDuplicateWeight(lastWeight, itemsWithWeight);
        }
        return weightedItems.get(lastWeight).get(0);
    }
    
    /**
     * Returns a random selection between items with equal weights.
     * 
     * @param weight The weight that has more than one item mapped to it.
     * @param itemsWithWeight The number of items mapped to that weight.
     * @return A randomly selected item from the list of items mapped to the
     * given weight.
     */
    private T resolveDuplicateWeight(int weight, int itemsWithWeight) {
            int randomNum = ThreadLocalRandom.current().nextInt(0, itemsWithWeight);
            return weightedItems.get(weight).get(randomNum);
    }
}
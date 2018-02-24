package weightedrandom.test;

import weightedrandom.main.WeightedRandom;

/**
 * An example of how to use WeightedRandom.
 * @author Marc Skaarup
 */
public class WeightedRandomDemo {
    
    public static void main(String[] args) {
        
        // Create the new WeightedRandom with the type of Object you'd like to
        // select from.
        WeightedRandom<Integer> rand = new WeightedRandom<>();
        
        // Use the add() method to populate the WeightedRandom. Here the
        // integers in the first argument are being added with the weight of the
        // second argument.
        rand.add(50, 50);
        rand.add(20, 20);
        rand.add(15, 15);
        rand.add(16, 15);
        //rand.add(item, weight);
        
        // These are being used to keep track of how many times each number has
        // been randomly selected.
        int numOf50 = 0;
        int numOf20 = 0;
        int numOf15 = 0;
        int numOf16 = 0;
        
        // The number of times a random item will be drawn. In this example, The 
        // larger this number is, the closer the resulting percentages will be 
        // to the number next to it.
        int numOfTimes = 10000000;
        
        for(int i = 0; i < numOfTimes; i++) {
            // The call to random() generats the random item.
            int temp = rand.next();
            
            // Checks the random item and logs it for the calculations below.
            switch (temp) {
                case 50: numOf50++; break;
                case 20: numOf20++; break;
                case 15: numOf15++; break;
                case 16: numOf16++; break;
                default: break;
            }
        }
        
        // Prints the percentage of times an item was selected throughout the
        // loop.
        System.out.println("Number | Percentage");
        System.out.println("50     | " + (((double)numOf50 / (double)numOfTimes) * 100) + "%");
        System.out.println("20     | " + (((double)numOf20 / (double)numOfTimes) * 100) + "%");
        System.out.println("15 #1  | " + (((double)numOf15 / (double)numOfTimes) * 100) + "%");
        System.out.println("15 #2  | " + (((double)numOf16 / (double)numOfTimes) * 100) + "%");
    }
}

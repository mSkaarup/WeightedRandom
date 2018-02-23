# WeightedRandom
WeightedRandom is a simple class for randomly selecting from a set of weighted objects. That is, each item added is also given a numerical weight which dictates it's likelyhood of being selected.

For instance, lets say you have a boss in a game that drops several items with different rarities. It can drop a shield or a sword, but you want the sword to be more likely to drop than the shield. You can give the sword a weight of 4 and the shield a weight of 1, which will make it so the sword is dropped 80% (4/5) of the time, and the shield is dropped 20% (1/5) of the time.

This supports multiple Objects with the same weight, with each Object that has the same weight being equally likely to be chosen.

## Usage
Simply create a new WeightedRandom object, populate it with weighted items, then call WeightedRandom.next() to randomly draw one of the items.
```java
WeightedRandom<String> random = new WeightedRandom<>();
String[] strings = {"hello", "world"};
int[] weights = {19, 1};

random.add(strings[0], weights[0]);
random.add(strings[1], weights[1]);

// Alternatively, you can add an array of objects and weights all at once.
random.add(strings, weights);

// Each call to next() will return a random Object in the list.
String result = random.next();

```
Additionally to this, you can also get a double representation of the chance for a specific Object to be selected using `random.getPercentage(T obj)` This will return a double between 0 and 1.

You can also remove objects using `random.remove(T obj)` or `random.remove(int weight)` or `random.remove(T obj, int weight)` The first will remove all instances of that object within the random Object. The second will remove every item with the given weight, and the third will remove the given object that has the given weight. When Objects are removed, the total weight decreases, and this results in percentages changing. For example, if you have two objects weighted at 50, both will have a 50% chance of being selected. If you remove one, the remaining object will be selected 100% of the time.

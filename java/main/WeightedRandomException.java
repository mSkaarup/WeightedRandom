package weightedrandom.main;

public class WeightedRandomException extends RuntimeException {
    
    public WeightedRandomException() {}
    
    public WeightedRandomException(String message) {
        super(message);
    }
    
    public WeightedRandomException(Throwable cause) {
        super(cause);
    }
    
    public WeightedRandomException(String message, Throwable cause) {
        super(message, cause);
    }
}

package shared;

/**
 * This class takes in the values stored for an Ingredient object
 */
public class Ingredient {
    private String ingredientName;

    public Ingredient(String ingredientName) {
        this.ingredientName = ingredientName;
    }

    /**
     *
     * @param ingredientName sets the string
     */
    public void setName(String ingredientName) {
        this.ingredientName = ingredientName;
    }

    /**
     *
     * @return string type
     */
    public String getName() {
        return ingredientName;
    }
}

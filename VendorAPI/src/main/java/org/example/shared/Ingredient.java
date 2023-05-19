package org.example.shared;

public class Ingredient {
    private String ingredientName;

    public Ingredient(String ingredientName) {
        this.ingredientName = ingredientName;
    }

    public void setName(String ingredientName) {
        this.ingredientName = ingredientName;
    }

    public String getName() {
        return ingredientName;
    }
}

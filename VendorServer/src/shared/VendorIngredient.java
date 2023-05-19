package org.example.shared;

public class VendorIngredient {
    private Vendor vendor;
    private Ingredient ingredient;
    private int price;

    public VendorIngredient(Vendor vendor, Ingredient ingredient, int price) {
        this.vendor = vendor;
        this.ingredient = ingredient;
        this.price = price;
    }

    public Ingredient getIngredient() {
        return ingredient;
    }

    public Vendor getVendor() {
        return vendor;
    }

    public int getPrice() {
        return price;
    }
}

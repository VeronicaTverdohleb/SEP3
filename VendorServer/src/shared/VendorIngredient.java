package shared;

public class VendorIngredient {
    private Vendor vendor;
    private Ingredient ingredient;
    private double price;

    public VendorIngredient(Vendor vendor, Ingredient ingredient, double price) {
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

    public double getPrice() {
        return price;
    }
}

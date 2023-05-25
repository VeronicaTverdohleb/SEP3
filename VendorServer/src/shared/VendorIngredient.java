package shared;

/**
 * This class takes in the values stored for a VendorIngredient object
 */
public class VendorIngredient {
    private Vendor vendor;
    private Ingredient ingredient;
    private double price;

    /**
     * This is a 3 argument constructor
     * @param vendor takes in a Vendor
     * @param ingredient takes in an Ingredient
     * @param price takes in an integer
     */
    public VendorIngredient(Vendor vendor, Ingredient ingredient, double price) {
        this.vendor = vendor;
        this.ingredient = ingredient;
        this.price = price;
    }

    /**
     *
     * @return Ingredient type
     */
    public Ingredient getIngredient() {
        return ingredient;
    }
    /**
     *
     * @return Vendor type
     */
    public Vendor getVendor() {
        return vendor;
    }
    /**
     *
     * @return int type
     */
    public double getPrice() {
        return price;
    }
}

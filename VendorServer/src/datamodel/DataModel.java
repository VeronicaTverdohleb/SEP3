package datamodel;

import shared.Ingredient;
import shared.VendorIngredient;

import java.sql.SQLException;
import java.util.ArrayList;

public interface DataModel {
    ArrayList<VendorIngredient> getVendors(String ingredientName) throws SQLException;
}

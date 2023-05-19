package org.example.datamodel;

import org.example.shared.Ingredient;
import org.example.shared.VendorIngredient;

import java.sql.SQLException;
import java.util.ArrayList;

public interface DataModel {
    ArrayList<VendorIngredient> getVendors(String ingredientName) throws SQLException;
}

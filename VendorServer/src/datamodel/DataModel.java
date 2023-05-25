package datamodel;

import shared.VendorIngredient;


import java.sql.SQLException;
import java.util.ArrayList;

/**
 * Interface implemented by model.DataModelManager
 */
public interface DataModel {
    ArrayList<VendorIngredient> getVendors(String ingredientName) throws SQLException;
}

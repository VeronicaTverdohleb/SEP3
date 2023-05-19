package org.example.datamodel;

import org.example.shared.VendorIngredient;

import java.sql.SQLException;
import java.util.ArrayList;

public class DataModelManager implements DataModel {
    @Override
    public ArrayList<VendorIngredient> getVendors(String ingredientName) throws SQLException {
        return null;
    }
}

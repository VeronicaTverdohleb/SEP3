package org.example.model;

import org.example.datamodel.DataModel;
import org.example.shared.VendorIngredient;
import org.json.simple.JSONObject;

import java.sql.SQLException;
import java.util.ArrayList;

public class ModelManager implements Model {
    private DataModel dataModel;

    public ModelManager(DataModel dataModel) {
        this.dataModel = dataModel;
    }

    public JSONObject convertVendorObjectsIntoJSON(VendorIngredient vendorIngredient) {
        return null;
    }

    @Override
    public JSONObject getVendors(String ingredientName) {
        ArrayList<VendorIngredient> vendorIngredient = null;
        try {
           vendorIngredient = dataModel.getVendors(ingredientName);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        return null;
    }
}

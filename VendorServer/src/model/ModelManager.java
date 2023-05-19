package model;
import java.util.HashMap;
import java.util.Map;

import datamodel.DataModel;
import shared.VendorIngredient;
import org.json.simple.JSONObject;

import java.sql.SQLException;
import java.util.ArrayList;

public class ModelManager implements Model {
    private DataModel dataModel;

    public ModelManager(DataModel dataModel) {
        this.dataModel = dataModel;
    }



    @Override
    public JSONObject getVendors(String ingredientName) {
        ArrayList<VendorIngredient> vendors = null;
        try {
            vendors = dataModel.getVendors(ingredientName);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        return convertVendorsIntoJSON(vendors);
    }

    /***
     * Translates DB output to JSON string
     * The format of the JSON output is:
     * {"vendor": [ {"vendorName: "{vendorName}",
     *               "ingredientName": "{ingredientName}",
     *               "price": "{price}"}
     *            ]
     * }
     * @param vendorIngredients
     * @return
     */
    public JSONObject convertVendorsIntoJSON(ArrayList<VendorIngredient> vendorIngredients) {
        // Creating map for "vendor": [list of vendors and their info]
        Map<String, ArrayList<Map<String, String>>> vendorsInfo = new HashMap<>();

        // Creating Maps for each vendor and putting it in the above Map vendorInfo
        for (VendorIngredient vendorIngredient : vendorIngredients) {
            Map<String, String> singleVendorInfo = new HashMap<>();
            // Putting values from DB output together with pre-agreed keys compatible with the C# side
            singleVendorInfo.put("vendorName", vendorIngredient.getVendor().getVendorName());
            singleVendorInfo.put("ingredientName", vendorIngredient.getIngredient().getName());
            singleVendorInfo.put("price", String.valueOf(vendorIngredient.getPrice()));
            vendorsInfo.computeIfAbsent("vendor", k -> new ArrayList<>()).add(singleVendorInfo);
        }

        return new JSONObject(vendorsInfo);
    }


}

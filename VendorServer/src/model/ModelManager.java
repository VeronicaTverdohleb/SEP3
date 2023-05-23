package model;
import java.nio.charset.StandardCharsets;
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
    public byte[] getVendors(String ingredientName) {
        ArrayList<VendorIngredient> vendors = null;
        try {
            vendors = dataModel.getVendors(ingredientName);
        } catch (SQLException e) {
            throw new RuntimeException(e);
        }

        return convertVendorsIntoByte(vendors);
    }

    /***
     * Translates DB output to JSON string and then into byte array
     * The format of the JSON output is:
     * {"vendor": [ {"vendorName: "{vendorName}",
     *               "ingredientName": "{ingredientName}",
     *               "price": "{price}"}
     *            ]
     * }
     * @param vendorIngredients
     * @return
     */
    public byte[] convertVendorsIntoByte(ArrayList<VendorIngredient> vendorIngredients) {
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

        // Map into JSON, so it has "" around keys and values (otherwise it just makes the values as Ingredient=Tomato)
        JSONObject json = new JSONObject(vendorsInfo);

        // Return byte array of the JSON
        return json.toString().getBytes(StandardCharsets.UTF_8);
    }


}

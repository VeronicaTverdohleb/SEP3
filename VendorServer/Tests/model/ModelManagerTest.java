package model;

import datamodel.DataModel;
import datamodel.DataModelManager;
import org.json.simple.JSONObject;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import shared.Ingredient;
import shared.Vendor;
import shared.VendorIngredient;

import java.nio.charset.StandardCharsets;
import java.sql.SQLException;
import java.util.ArrayList;

import static org.assertj.core.api.FactoryBasedNavigableListAssert.assertThat;
import static org.junit.jupiter.api.Assertions.*;
import static org.mockito.Mockito.when;

class ModelManagerTest {

    @Mock
    DataModel model;
    @Mock DataModel dataModel;
    @Mock ModelManager modelManager;
    @Mock
    VendorIngredient vendorIngredient;
    @Mock
    VendorIngredient vendorIngredient1;
    @Mock
    Ingredient ingredient;
    @Mock
    Ingredient ingredient1;
    @Mock
    Vendor vendor;
    @Mock Vendor vendor1;

    @BeforeEach
    public void setup() {
        MockitoAnnotations.openMocks(this);
        modelManager=new ModelManager(dataModel);
    }


    @Test
    void getVendors_O() throws SQLException {
        String name="Tomato";
        ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
        vendor=new Vendor("Netto");
        ingredient=new Ingredient("Cheese");
        vendorIngredient=new VendorIngredient(vendor,ingredient,20.9);
        vendorIngredients.add(vendorIngredient);

        when(dataModel.getVendors(name)).thenReturn(vendorIngredients);

        byte[] result=modelManager.getVendors(name);

        assertNotNull(result);
    }

    @Test
    void convertVendorsIntoByte_Z() {
        ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();

        byte[] result= modelManager.convertVendorsIntoByte(vendorIngredients);
        String expectedJsonString= "{}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }

    @Test
    void convertVendorsIntoByte_O() {
        ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
        vendor=new Vendor("Netto");
        ingredient=new Ingredient("Cheese");
        vendorIngredient=new VendorIngredient(vendor,ingredient,20.9);
        vendorIngredients.add(vendorIngredient);


        byte[] result= modelManager.convertVendorsIntoByte(vendorIngredients);
        String expectedJsonString= "{\"vendor\":[{\"ingredientName\":\"Cheese\",\"price\":\"20.9\",\"vendorName\":\"Netto\"}]}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }

    @Test
    void convertVendorsIntoByte_M() {
        ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
        vendor=new Vendor("Netto");
        ingredient=new Ingredient("Cheese");
        vendorIngredient=new VendorIngredient(vendor,ingredient,20.9);
        vendor1=new Vendor("Bilka");
        ingredient1=new Ingredient("Cheese");
        vendorIngredient1=new VendorIngredient(vendor1,ingredient1,21.0);

        vendorIngredients.add(vendorIngredient);
        vendorIngredients.add(vendorIngredient1);


        byte[] result= modelManager.convertVendorsIntoByte(vendorIngredients);
        String expectedJsonString= "{\"vendor\":[{\"ingredientName\":\"Cheese\",\"price\":\"20.9\",\"vendorName\":\"Netto\"},{\"ingredientName\":\"Cheese\",\"price\":\"21.0\",\"vendorName\":\"Bilka\"}]}";

        byte[] expectedBytes = expectedJsonString.getBytes(StandardCharsets.UTF_8);

        assertArrayEquals(expectedBytes,result);


    }
}
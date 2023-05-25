package datamodel;

import static org.junit.jupiter.api.Assertions.*;

import client.Client;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.api.extension.ExtendWith;
import org.junit.jupiter.api.function.Executable;
import org.mockito.Mock;
import org.mockito.MockitoAnnotations;
import org.mockito.junit.jupiter.MockitoExtension;
import shared.Ingredient;
import shared.Vendor;
import shared.VendorIngredient;

import java.sql.SQLException;
import java.util.ArrayList;

import static org.mockito.Mockito.when;

@ExtendWith(MockitoExtension.class)
class DataModelManagerTest {
    @Mock DataModelManager dataModelManager;
    @Mock DataModel dataModel;
    @Mock VendorIngredient vendorIngredient;
    @Mock Ingredient ingredient;
    @Mock Vendor vendor;
    @Mock
    Client client;

    @BeforeEach public void setup() throws SQLException {
        MockitoAnnotations.openMocks(this);
        dataModelManager=new DataModelManager();
    }

    @Test public void testGettingVendors_Z() throws SQLException {

        String name="Tomato";
        ArrayList<VendorIngredient> vendors=new ArrayList<>();

        try {
            when(dataModel.getVendors(name)).thenReturn(vendors);
        } catch (Exception e) {
            e.printStackTrace();
        }
        ArrayList<VendorIngredient> actualResult= dataModel.getVendors(name);

        assertEquals(vendors,actualResult);
        assertThrows(Exception.class, ()->{ throw new Exception("No vendors");});

    }

        @Test public void testGettingVendors_O() throws SQLException {
            String name="Tomato";
            ingredient=new Ingredient("Tomato");
            Vendor v =new Vendor("netto");
            VendorIngredient vi1=new VendorIngredient(v,ingredient,9.99);
            ArrayList<VendorIngredient> vendors=new ArrayList<>();
            vendors.add(vi1);
            ArrayList<VendorIngredient> vendors1=new ArrayList<>();
            vendors1.add(vi1);

            try {
                when(dataModel.getVendors(name)).thenReturn(vendors);
            } catch (Exception e) {
                e.printStackTrace();
            }
            ArrayList<VendorIngredient> actualResult= dataModel.getVendors(name);

             assertEquals(vendors1,actualResult);
    }

    @Test public void testGettingVendors_M() throws SQLException {
        String name="Tomato";
        ingredient=new Ingredient("Tomato");
        Vendor v =new Vendor("netto");
        Vendor v1=new Vendor("bilka");
        VendorIngredient vi1=new VendorIngredient(v,ingredient,9.99);
        VendorIngredient vi2=new VendorIngredient(v1,ingredient,9.29);

        ArrayList<VendorIngredient> vendors=new ArrayList<>();
        vendors.add(vi1);
        vendors.add(vi2);
        ArrayList<VendorIngredient> vendors1=new ArrayList<>();
        vendors1.add(vi1);

        try {
            when(dataModel.getVendors(name)).thenReturn(vendors);
        } catch (Exception e) {
            e.printStackTrace();
        }
        ArrayList<VendorIngredient> actualResult= dataModel.getVendors(name);

        assertEquals(vendors,actualResult);
    }



}
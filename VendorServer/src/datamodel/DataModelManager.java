package datamodel;

import shared.Ingredient;
import shared.Vendor;
import shared.VendorIngredient;

import java.sql.*;
import java.util.ArrayList;

public class DataModelManager implements DataModel {

    public DataModelManager()throws SQLException{
        DriverManager.registerDriver(new org.postgresql.Driver());
    }

    private Connection getConnection() throws SQLException{
        return DriverManager.getConnection("jdbc:postgresql://localhost:5433/vendor_db","postgres","bobs");
    }
    @Override
    public ArrayList<VendorIngredient> getVendors(String ingredientName) throws SQLException {
        System.out.println("in datamodelmanager");
        try(Connection connection=getConnection()){
            PreparedStatement preparedStatement=connection.prepareStatement("select * " +
                    " from vendoringredient" +
                    " where ingredientname= "+ "'" +ingredientName+ "'") ;
            System.out.println(preparedStatement);
            ResultSet resultSet=preparedStatement.executeQuery();
            ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
            while (resultSet.next()){
                String vendorName=resultSet.getString(1);
                String ingName=resultSet.getString(2);
                double price=resultSet.getDouble(3);
                Vendor vendor=new Vendor(vendorName);
                Ingredient ingredient=new Ingredient(ingName);
                VendorIngredient vIng=new VendorIngredient(vendor,ingredient,price);

                vendorIngredients.add(vIng);

            }
            return vendorIngredients;
        }

    }
}

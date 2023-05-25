package datamodel;

import shared.Ingredient;
import shared.Vendor;
import shared.VendorIngredient;

import java.sql.*;
import java.util.ArrayList;

/**
 * Implements DataModel interface
 * Connects to the database in order to retrieve vendors
 */
public class DataModelManager implements DataModel {

    /**
     * Public constructor ensuring the database driver connection
     * @throws SQLException
     */
    public DataModelManager()throws SQLException{
        DriverManager.registerDriver(new org.postgresql.Driver());
    }

    /**
     * Gets the connection with the database
     * @return Connection of the database
     * @throws SQLException
     */
    private Connection getConnection() throws SQLException{
        return DriverManager.getConnection("jdbc:postgresql://localhost:5432/vendor_db","postgres","bobs");
    }

    /**
     * Method that returns an ArrayList of all the vendors from the database
     * who have the requested ingredient
     * @param ingredientName the ingredients name which will be requested
     * @return vendor (name, ingredient, price)
     * @throws SQLException
     */
    @Override
    public ArrayList<VendorIngredient> getVendors(String ingredientName) throws SQLException {
        try(Connection connection=getConnection()){
            PreparedStatement preparedStatement=connection.prepareStatement("select * " +
                    " from vendoringredient" +
                    " where ingredientname= "+ "'" +ingredientName+ "'") ;
            //System.out.println(preparedStatement);
            ResultSet resultSet=preparedStatement.executeQuery();
            ArrayList<VendorIngredient> vendorIngredients=new ArrayList<>();
            while (resultSet.next()){
                String vendorName=resultSet.getString(1);
                if(vendorName==null){
                    throw new SQLException("No vendors for this ingredient");
                }
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

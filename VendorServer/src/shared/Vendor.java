package shared;

/**
 * This class takes in the values stored for a Vendor object
 */
public class Vendor {
    private String vendorName;

    public Vendor(String vendorName) {
        this.vendorName = vendorName;
    }

    /**
     *
     * @return string type
     */
    public String getVendorName() {
        return vendorName;
    }

    /**
     *
     * @param vendorName string type
     */
    public void setVendorName(String vendorName) {
        this.vendorName = vendorName;
    }
}

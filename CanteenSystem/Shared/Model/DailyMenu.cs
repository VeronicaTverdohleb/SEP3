namespace Shared.Model;

public class DailyMenu
{
    public List<Item> Items { get; set; }
    public DateTime Date { get; set; }
    
    private DailyMenu() {}

}
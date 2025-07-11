using ProjectSoftwareWorkshop.Models.Category;
using ProjectSoftwareWorkshop.Models.Shop;

namespace ProjectSoftwareWorkshop.Models.Purchase;

public class PurchaseDto
{
    public int Id { get; set; }

    //public int ShopId { get; set; }
    public required ShopDto Shop { get; set; }

    public double BillCost { get; set; }

    // public int CategoryId { get; set; }
    public required CategoryDto Category { get; set; }
    
    public required DateTime Date { get; set; }

    public required string Note { get; set; }
}
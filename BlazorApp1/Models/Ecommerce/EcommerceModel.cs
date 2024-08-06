namespace BlazorApp1.Models.Ecommerce;

public class EcommerceModel
{


    public EcommerceModel()
    {
        
    }
    public EcommerceModel(string Product, int ProductId, EcommerceStatus status)
    {
        this.Product = Product;
        this.ProductId = ProductId;
        this.status = status;
    }

    public string Product { get; set; }
    public int ProductId { get; set; }
    public EcommerceStatus status { get; set; }
}

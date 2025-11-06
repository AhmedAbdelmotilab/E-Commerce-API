namespace E_Commerce.Shared.DTOs.ProductDTOs ;

public class ProductDto
{
    public int Id { get ; set ; }
    private string Name { get ; set ; } = default! ;
    public string Description { get ; set ; } = default! ;
    public string PictureUrl { get ; set ; } = default! ;
    public decimal Price { get ; set ; } = default! ;
    public string ProductType { get ; set ; } = default! ;
    public string ProductBrand { get ; set ; } = default! ;
}
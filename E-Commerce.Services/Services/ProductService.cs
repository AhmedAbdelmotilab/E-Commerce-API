using AutoMapper ;
using E_Commerce.Domain.Contracts ;
using E_Commerce.Domain.Entities.ProductModule ;
using E_Commerce.Services_Abstraction ;
using E_Commerce.Services.Specifications ;
using E_Commerce.Shared.DTOs.ProductDTOs ;

namespace E_Commerce.Services.Services ;

public class ProductService : IProductService
{
    private readonly IMapper _mapper ;
    private readonly IUnitOfWork _unitOfWork ;

    public ProductService ( IUnitOfWork unitOfWork , IMapper mapper )
    {
        _unitOfWork = unitOfWork ;
        _mapper = mapper ;
    }

    public async Task < IEnumerable < ProductDto > > GetProductsAsync ( )
    {
        var Specification = new ProductWithTypeAndBrandSpecification ( ) ;
        var Products = await _unitOfWork.GetRepository < Product , int > ( ).GetAllAsync ( Specification ) ;
        return _mapper.Map < IEnumerable < ProductDto > > ( Products ) ;
    }

    public async Task < ProductDto > GetProductByIdAsync ( int id )
    {
        var Specification = new ProductWithTypeAndBrandSpecification ( id) ;
        var Product = await _unitOfWork.GetRepository < Product , int > ( ).GetByIdAsync ( Specification ) ;
        return _mapper.Map < ProductDto > ( Product ) ;
    }

    public async Task < IEnumerable < BrandDto > > GetAllBrandsAsync ( )
    {
        var Brands = await _unitOfWork.GetRepository < ProductBrand , int > ( ).GetAllAsync ( ) ;
        return _mapper.Map < IEnumerable < BrandDto > > ( Brands ) ;
    }

    public async Task < IEnumerable < TypeDto > > GetAllTypesAsync ( )
    {
        var Types = await _unitOfWork.GetRepository < ProductType , int > ( ).GetAllAsync ( ) ;
        return _mapper.Map < IEnumerable < TypeDto > > ( Types ) ;
    }
}

using AutoMapper;
using Core;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IGenericRepository<Product> productsRepo;
    private readonly IGenericRepository<ProductBrand> productBrandRepo;
    private readonly IGenericRepository<ProductType> productTypeRepo;
    private readonly IMapper mapper;

    public ProductsController(
        IGenericRepository<Product> productsRepo,
        IGenericRepository<ProductBrand> productBrandRepo,
        IGenericRepository<ProductType> productTypeRepo,
        IMapper mapper
        )
    {
        this.productsRepo = productsRepo;
        this.productBrandRepo = productBrandRepo;
        this.productTypeRepo = productTypeRepo;
        this.mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
    {
        var spec = new ProductsWithTypesAndBrandsSpecification();
        var products = await productsRepo.ListAsync(spec);


        return Ok(mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
    }

    [HttpGet("{id}")]

    public async Task<ActionResult<ProductToReturnDto>> GetProducts(int id)
    {
        var spec = new ProductsWithTypesAndBrandsSpecification(id);

        var product = await productsRepo.GetEntityWithSpec(spec);

        return mapper.Map<Product, ProductToReturnDto>(product);

    }

    [HttpGet("brands")]
    public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
    {
        return Ok(await productBrandRepo.ListallAsync());
    }

    [HttpGet("types")]
    public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
    {
        return Ok(await productTypeRepo.ListallAsync());
    }
}


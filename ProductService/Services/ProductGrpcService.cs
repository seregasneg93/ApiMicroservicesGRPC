using Contracts;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ProductService.Data;

namespace ProductService.Services
{
    public class ProductGrpcService(ProductDbContext context) : ProductServiceRpc.ProductServiceRpcBase
    {
        private readonly ProductDbContext _context = context;

        public override async Task<AddProductResponse> AddProduct(AddProductRequest request, ServerCallContext context)
        {
            var product = new Models.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = Convert.ToDecimal(request.Price)
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return new AddProductResponse
            {
                Success = true,
                Message = "Товар успешно добавлен"
            };
        }

        public override async Task<ProductListResponse> GetAll(Empty request, ServerCallContext context)
        {
            var products = await _context.Products.ToListAsync();

            var response = new ProductListResponse();
            response.Products.AddRange(products.Select(p => new Contracts.Product
            {
                Name = p.Name,
                Description = p.Description,
                Price = Convert.ToDouble(p.Price)
            }));

            return response;
        }
    }
}

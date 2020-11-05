using EleterosEB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class ProductService
    {
        private readonly IUnitOfWork _unitOfwork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }

        public Task<bool> CreateProduct(Product newproduct)
        {
            _unitOfwork.ProductRepository.Add(newproduct);
            return _unitOfwork.CommitAsync();

        }

        public Task<bool> DeleteProduct(Product product)
        {
            _unitOfwork.ProductRepository.Delete(product);
            return _unitOfwork.CommitAsync();
        }

        public Task<bool> UpdateProduct(Product product)
        {
            _unitOfwork.ProductRepository.Update(product);
            return _unitOfwork.CommitAsync();
        }

        public async Task<IReadOnlyList<Product>> GetAllProductsAsync()
        {
            return await _unitOfwork.ProductRepository.ListAsync();
        }

        public Task<Product> GetProductByIdAsync(int id)
        {
            return _unitOfwork.ProductRepository.GetByIdAsync(id);
        }

        public async Task<Product> GetProductByNameAsync(string name)
        {
            var query = await _unitOfwork.ProductRepository.ListAsync();

            return query.FirstOrDefault(r => r.ProductName == name);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Data;
using EleterosEB.Domain;

namespace EleterosEB.Bll
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfwork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfwork = unitOfWork;
        }

        public Task<bool> CreateCategory(Category newCategory)
        {
            _unitOfwork.CategoryRepository.Add(newCategory);
            return _unitOfwork.CommitAsync();

        }

        public Task<bool> DeleteCategory(Category category)
        {
            _unitOfwork.CategoryRepository.Delete(category);
            return _unitOfwork.CommitAsync();
        }

        public Task<bool> UpdateCategory(Category category)
        {
            _unitOfwork.CategoryRepository.Update(category);
            return _unitOfwork.CommitAsync();
        }

        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        {
            return await _unitOfwork.CategoryRepository.ListAsync();
        }

        public Task<Category> GetCategoryByIdAsync(int id)
        {
            return _unitOfwork.CategoryRepository.GetByIdAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            var query = await _unitOfwork.CategoryRepository.ListAsync();

            return query.FirstOrDefault(r => r.CategoryName == name);
        }
    }
}

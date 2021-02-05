using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EleterosEB.Bll;
using EleterosEB.Data;
using EleterosEB.Data.Repositories.Intefaces;
using EleterosEB.Domain;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace EleterosEB.UnitTests
{
    public class TestUnitOfWork: IUnitOfWork
    {
        public IAppointmentRepository AppointmentRepository { get; }
        public IAppointmentTypeRepository AppointmentTypeRepository { get; }
        public IClientRepository ClientRepository { get; }
        public IDoctorRepository DoctorRepository { get; }
        public IPatientRepository PatientRepository { get; }
        public IRoomRepository RoomRepository { get; }
        public ICategoryRepository CategoryRepository { get; }
        public IProductRepository ProductRepository { get; }
        public ISurgeryRoomAppointmentRepository SurgeryRoomBooking { get; }

        public Task<bool> CommitAsync()
        {
            throw new NotImplementedException();
        }

        public TestUnitOfWork(IProductRepository productRepository)
        {
            this.ProductRepository = productRepository;
        }
    }

    public class TestProductRepository : IProductRepository
    {
        private readonly List<Product> fakeDataSource;

        public TestProductRepository(List<Product> products)
        {
            this.fakeDataSource = products;
        }

        public void Add(Product entity)
        {
            fakeDataSource.Add(new Product()
            {
                ProductId = entity.ProductId,
                ProductName = entity.ProductName,
                CategoryId = entity.CategoryId,
                QuantityPerUnit = entity.QuantityPerUnit,
                UnitPrice = entity.UnitPrice,
                UnitInStock = entity.UnitInStock
            });
        }

        public void Update(Product entity)
        {
            var product = fakeDataSource.FirstOrDefault(p => p.ProductId == entity.ProductId);
            if (product == null) return;

            product.ProductId = entity.ProductId;
            product.ProductName = entity.ProductName;
            product.CategoryId = entity.CategoryId;
            product.QuantityPerUnit = entity.QuantityPerUnit;
            product.UnitPrice = entity.UnitPrice;
            product.UnitInStock = entity.UnitInStock;

        }

        public void Delete(Product entity)
        {
            fakeDataSource.Remove(entity);
        }

        public Task<Product> GetByIdAsync(int id)
        {
            return (Task<Product>)fakeDataSource.Where(p => p.ProductId == id);
        }

        public Task<IReadOnlyList<Product>> ListAsync(params Expression<Func<Product, object>>[] includes)
        {
            return fakeDataSource.AsQueryable().ToListAsync();
        }
    }


    [TestFixture]
    public class ProductServiceTest
    {
        [Test]
        public async Task TestGetAllProductsAsync()
        {
            var input = new List<Product>
            {
                new Product { ProductId = 1, ProductName = "prodName-1", CategoryId = 1, QuantityPerUnit = "1db", UnitInStock = 1, UnitPrice = 1},
                new Product { ProductId = 2, ProductName = "prodName-2", CategoryId = 2, QuantityPerUnit = "2db", UnitInStock = 3, UnitPrice = 1},
            };

            var m = new ProductService(
                new TestUnitOfWork(
                    new TestProductRepository(input)));

            var output = await m.GetAllProductsAsync();

            Assert.That(output.Count == 2);
            Assert.That(output.Single().ProductId == 2);
        }

    }
}

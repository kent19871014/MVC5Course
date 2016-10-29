using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ProductRepository : EFRepository<Product>, IProductRepository
	{
        public override IQueryable<Product> All()
        {
            return base.All().Where(p => p.IsDeleted == false);
        }
        public Product Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ProductId == id);
        }

        public IQueryable<Product> Get所有資料依據_ProductID排序(int n)
        {
            return this.All().Take(n).OrderByDescending(p => p.ProductId);
        }
    }

	public  interface IProductRepository : IRepository<Product>
	{

	}
}
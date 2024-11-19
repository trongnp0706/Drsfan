﻿using System;
using System.Linq;
using Drsfan.DataAcess.Data;
using Drsfan.DataAcess.Repository.IRepository;
using Drsfan.Models;

namespace Drsfan.DataAcess.Repository
{
    public class ProductRepo : Repository<Product>, IProductRepo
    {
        private DrsfanDbContext _db;

        public ProductRepo(DrsfanDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb = _db.Products.FirstOrDefault(s => s.Id == obj.Id);
            if (objFromDb != null)
            {
                // Cập nhật trường ImageUrl nếu có thay đổi
                if (!string.IsNullOrEmpty(obj.ImageUrl))
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }

                // Cập nhật các trường khác
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Brand = obj.Brand;
                objFromDb.ModelNumber = obj.ModelNumber;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.DiscountPrice = obj.DiscountPrice;
                objFromDb.WarrantyPeriod = obj.WarrantyPeriod;
                objFromDb.Features = obj.Features;
                objFromDb.PowerConsumption = obj.PowerConsumption;
                objFromDb.CategoryId = obj.CategoryId;
            }
        }
    }
}

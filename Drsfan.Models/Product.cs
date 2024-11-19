﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Drsfan.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }  

        public string Description { get; set; }

        public string Brand { get; set; }  

        public string ModelNumber { get; set; }  

        [Required]
        public double ListPrice { get; set; }  

        
        [Required]
        [Display(Name = "Discount Price")]
        public double DiscountPrice { get; set; }  


        [Required]
        [Display(Name = "Warranty Period")]
        public string WarrantyPeriod { get; set; }  // Thời gian bảo hành

        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

        
        public string Features { get; set; }  // Các tính năng đặc biệt của thiết bị

        public string PowerConsumption { get; set; }  // Công suất tiêu thụ điện
    }
}

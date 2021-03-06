﻿namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [Key]
        public int ProductCode { get; set; }

        [Required(ErrorMessage = "Không được để trống tên sản phẩm")]
        [StringLength(100)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Không được để trống mục này")]
        [StringLength(100)]
        public string MetaTitle { get; set; }

        [Required(ErrorMessage = "Không được để trống mục này")]
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Không được để trống mục này")]
        [Column(TypeName = "ntext")]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = "Không được để trống mục này")]
        [StringLength(250)]
        public string ImageFirst { get; set; }

        [StringLength(250)]
        public string ImageSecond { get; set; }

        [Required(ErrorMessage = "Không được để trống mục này")]
        public decimal Price { get; set; }

        public double? PercentSale { get; set; }

        public bool Promotion { get; set; }

        [Required(ErrorMessage = "Không được để trống mục này")]
        [StringLength(10)]
        public string CategoryIDParent { get; set; }

        [StringLength(10)]
        public string CategoryIDChild { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? Quanity { get; set; }

        public bool Status { get; set; }

        public int? BuyCount { get; set; }

        public int? ViewCount { get; set; }

        public double? ReviewPoint { get; set; }
    }
}

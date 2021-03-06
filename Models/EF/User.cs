﻿namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên đăng nhập")]
        [StringLength(20)]
        public string Username { get; set; }

        [Required(ErrorMessage = "Vui lòng điền mật khẩu")]
        [StringLength(50)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Vui lòng điền tên chủ tài khoản")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDay { get; set; }

        [Required(ErrorMessage = "Vui lòng điền email")]
        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(20)]
        public string Mobile { get; set; }

        public int? ProvinceID { get; set; }

        public int? DistrictID { get; set; }

        public int? SubDistrictID { get; set; }

        [Column(TypeName = "text")]
        public string Address { get; set; }

        public DateTime? CreatedDate { get; set; }

        [StringLength(50)]
        public string CreatedBy { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }

        public bool Status { get; set; }

        [Required(ErrorMessage = "vui lòng không để trống mục này")]
        public int TypeOfAccount { get; set; }

        public int? AmountMissOrder { get; set; }
    }
}

namespace Models.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Review")]
    public partial class Review
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProductCode { get; set; }

        [Key]
        [Column(Order = 1)]
        public Guid UserID { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string Text { get; set; }

        public double ReviewPoint { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}

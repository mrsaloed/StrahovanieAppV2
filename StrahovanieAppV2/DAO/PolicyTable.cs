namespace StrahovanieAppV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("PolicyTable")]
    public partial class PolicyTable
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int ClientID { get; set; }

        public int InsurerID { get; set; }

        public int CarID { get; set; }

        [Required]
        [StringLength(3)]
        public string Series { get; set; }

        public long Number { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateOfIssue { get; set; }

        [StringLength(11)]
        public string PaymentState { get; set; }

        public virtual Car Car { get; set; }

        public virtual Client Client { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Insurer Insurer { get; set; }
    }
}

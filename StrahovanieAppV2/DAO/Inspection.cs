namespace StrahovanieAppV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Inspection")]
    public partial class Inspection
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public int EmployeeID { get; set; }

        public int CarID { get; set; }

        [Column(TypeName = "date")]
        public DateTime InspectionDate { get; set; }

        [Required]
        [StringLength(20)]
        public string CarCondition { get; set; }

        public virtual Car Car { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual InspectionResult InspectionResult { get; set; }
    }
}

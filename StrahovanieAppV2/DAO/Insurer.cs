namespace StrahovanieAppV2
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Insurer")]
    public partial class Insurer
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Insurer()
        {
            PolicyTable = new HashSet<PolicyTable>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string InsurerName { get; set; }

        [Required]
        [StringLength(100)]
        public string InsurerAddress { get; set; }

        [StringLength(12)]
        public string PhoneNumber { get; set; }

        [Required]
        [StringLength(100)]
        public string License { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PolicyTable> PolicyTable { get; set; }
    }
}

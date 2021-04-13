namespace AdmissionsWebsite
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("AdmissionDecision")]
    public partial class AdmissionDecision
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public AdmissionDecision()
        {
            Students = new HashSet<Student>();
        }

        [Key]
        public int DecisionId { get; set; }

        [Display(Name = "Admission Decision")]
        [Required]
        [StringLength(20)]
        public string DecisionType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Student> Students { get; set; }
    }
}

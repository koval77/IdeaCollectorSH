namespace IdeaCollectorSH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Idea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Idea()
        {
            Comments = new HashSet<Comment>();
            Documents = new HashSet<Document>();
        }

        public int IdeaID { get; set; }

        [Required]
        public string IdeaTitle { get; set; }

        public DateTime SubmitDate { get; set; }

        public string Category { get; set; }

        public int? TUp { get; set; }

        public int? TDown { get; set; }

        public DateTime? ExpiryDate { get; set; }

        public int? ViewCount { get; set; }

        public int? StaffID { get; set; }

        public string IdeaDescription { get; set; }

        [StringLength(50)]
        public string AuthorEmail { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }

        public virtual Staff Staff { get; set; }
    }
}

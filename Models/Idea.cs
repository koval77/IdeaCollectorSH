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
        [Display(Name="Title")]
        public string IdeaTitle { get; set; }

        public DateTime SubmitDate { get; set; }

        public string Category { get; set; }

        [Display(Name="Likes")]
        public int? TUp { get; set; }
        [Display(Name="Dislikes")]
        public int? TDown { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Display(Name="Views")]
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

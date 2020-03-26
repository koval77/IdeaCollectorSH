//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace IdeaCollectorSH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Idea
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Idea()
        {
            this.Comments = new HashSet<Comment>();
            this.Documents = new HashSet<Document>();
        }
    
        public int IdeaID { get; set; }
        [Display(Name="Title for your idea:")]
        public string IdeaTitle { get; set; }
        public System.DateTime SubmitDate { get; set; }
        public string Category { get; set; }
        public Nullable<int> TUp { get; set; }
        public Nullable<int> TDown { get; set; }
        public Nullable<System.DateTime> ExpiryDate { get; set; }
        public Nullable<int> ViewCount { get; set; }
        public Nullable<int> StaffID { get; set; }
        public string IdeaDescription { get; set; }
        [Display(Name ="Author's E-mail:")]
        public string AuthorEmail { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Document> Documents { get; set; }
        public virtual Staff Staff { get; set; }
    }
}

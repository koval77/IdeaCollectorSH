namespace IdeaCollectorSH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Web;

    public partial class Document
    {
        [Key]
        public int DocumentID { get; set; }

        public int? IdeaID { get; set; }

        [StringLength(100)]
        public string DocumentName { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Please select file.")]
        public HttpPostedFileBase PostedFile { get; set; }

        public virtual Idea Idea { get; set; }
    }
}

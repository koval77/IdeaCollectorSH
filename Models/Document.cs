namespace IdeaCollectorSH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Document
    {
        public int DocumentID { get; set; }

        public int? IdeaID { get; set; }

        [StringLength(50)]
        public string DocumentName { get; set; }

        public virtual Idea Idea { get; set; }
    }
}

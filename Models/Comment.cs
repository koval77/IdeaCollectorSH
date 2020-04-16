namespace IdeaCollectorSH.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Comment
    {
        public int CommentId { get; set; }

        [Required]
        [StringLength(50)]
        public string CommentContent { get; set; }

        public DateTime SubmitDate { get; set; }

        public int? IdeaID { get; set; }

        public int? StaffID { get; set; }

        public virtual Idea Idea { get; set; }

        public virtual Staff Staff { get; set; }
    }
}

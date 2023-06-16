using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace crucibleBlog.Models
{
    public class BlogPost
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 2)]
        public string? Title { get; set; }

        [StringLength(600, ErrorMessage = "The {0} must be at least {2} and max {1} characters long.", MinimumLength = 2)]
        public string? Abstract { get; set; }

        [Required]
        public string? Content { get; set; }
         
        [Required]
        [Display(Name = "Created Date")]
        [DataType(DataType.Date)]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Updated Date")]
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; }

        public string? Slug { get; set; }

        [Display(Name = "Deleted?")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Published?")]
        public bool IsPublished { get; set; }

        public int CategoryId { get; set; }

        // Image Properties
        [NotMapped]
        public virtual IFormFile? ImageFile { get; set; }
        public virtual byte[]? ImageData { get; set; }
        public virtual string? ImageType { get; set; }
    
        public virtual Category? Category { get; set; }
    
        public virtual ICollection<Tag> Tags { get; set; } = new HashSet<Tag>();    

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

		public virtual ICollection<BlogLike> Likes { get; set; } = new HashSet<BlogLike>();
	}
}

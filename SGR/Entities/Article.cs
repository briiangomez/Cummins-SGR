using SGR.Pages;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGR.Entities
{
    public class Article
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdArticle { get; set; }

        [Required (ErrorMessage = "Título es requerido")]
        [StringLength (20, MinimumLength = 5,
                           ErrorMessage = "Título debe contener entre 5 y 20 caracteres")]
        public string Title { get; set; }
        
        [Required]
        [ForeignKey("IdCategory")]
        public Category Category { get; set; }

    }
}

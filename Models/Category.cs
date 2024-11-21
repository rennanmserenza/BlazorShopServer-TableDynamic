using System.ComponentModel.DataAnnotations;

namespace BlazorShopServer.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Informe o Título")]
        [MinLength(3, ErrorMessage = "A categoria deve ter pelo menos 3 caracteres")]
        [MaxLength(60, ErrorMessage = "A categoria deve ter no máximo 60 caracteres")]
        public string Title { get; set; } = string.Empty;
    }
}

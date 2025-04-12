using System.ComponentModel.DataAnnotations;

namespace BTeste.ViewModels{

public class EditorCategoryViewModel{

    [Required(ErrorMessage = "This Field is required.")]
    [StringLength(40, MinimumLength = 3, ErrorMessage = "This field must be 3 and 40 characters.")]

    public string Name { get; set; }

    [Required(ErrorMessage = "This Field is required.")]
    public string Slug { get; set; }

}
}
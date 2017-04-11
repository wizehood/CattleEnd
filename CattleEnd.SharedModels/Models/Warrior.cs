using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CattleEnd.SharedModels.Models
{
    public class Warrior
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int SpeciesId { get; set; }

        [ForeignKey("SpeciesId")]
        public virtual WarriorSpecies Species { get; set; }

        [StringLength(100, ErrorMessage = "Name must not be longer than 100 characters.")]
        [Required(ErrorMessage = "This field is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "This field is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email address.")]
        public string Email { get; set; }

        public bool Deleted { get; set; }
    }
}

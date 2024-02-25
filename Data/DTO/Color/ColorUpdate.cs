using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO.Color
{
    public class ColorUpdate
    {
        [Required(ErrorMessage = "Veuillez référencer une couleur.")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Un choix de nom est requis.")]
        [StringLength(64)]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Veuillez choisir une couleur. Ex : FFFFFF")]
        [StringLength(6)]
        public required string Hex { get; set; }
    }
}

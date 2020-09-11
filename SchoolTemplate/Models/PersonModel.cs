using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolTemplate.Models
{
    public class Personmodel
    {
        public string Voornaam { get; set; }
    
        [Required]
        public string Achternaam { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public DateTime Geboortedatum { get; set; }
    }
}
//Model voor het contactformulier
using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolTemplate.Models
{
    public class Personmodel
    {
        public string Voornaam { get; set; }
    
        [Required (ErrorMessage = "Achternaam is verplicht")]
        public string Achternaam { get; set; }

        [Required (ErrorMessage = "E-mail is verplicht")]
        [EmailAddress (ErrorMessage ="Geen geldig e-mail adres")]
        public string Email { get; set; }
        
        [Required (ErrorMessage = "Gelieve een bericht achter te laten om contact op te nemen.")]
        public string Bericht { get; set; }

        public DateTime Geboortedatum { get; set; }
    }
}
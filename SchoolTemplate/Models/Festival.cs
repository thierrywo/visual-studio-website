//Model voor database tabel festival + toevoeging artiesten en prijzen models

using System;
using System.Collections.Generic;

namespace SchoolTemplate.Database
{
  public class Festival
  {
    public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Beschrijving { get; set; }    

    public Decimal Prijs { get; set; }

    public string Headliners { get; set; }

    public string Minimum_leeftijd { get; set; }

    public string Plaatje { get; set; }

    public string Datum { get; set; }


    public List<Artiest> Artiesten { get; set; }

    public List<Prijs> Prijzen { get; set; }


  }
}

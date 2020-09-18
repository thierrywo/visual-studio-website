using System;

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

  }
}

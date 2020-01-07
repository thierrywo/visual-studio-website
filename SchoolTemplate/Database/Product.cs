using System;

namespace SchoolTemplate.Database
{
  public class Product
  {
    public int Id { get; set; }
    
    public string Naam { get; set; }

    public string Formaat { get; set; }    

    /// <summary>
    /// Gebruik altijd decimal voor geldzaken. Dit doe je om te voorkomen dat er afrondingsfouten optreden
    /// </summary>
    public Decimal Prijs { get; set; }

    public int Gewicht { get; set; }

    public float Calorieen { get; set; }

  }
}

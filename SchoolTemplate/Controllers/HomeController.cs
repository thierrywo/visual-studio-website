using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
  public class HomeController : Controller
  {
    string connectionString = "Server=informatica.st-maartenscollege.nl ;Port=3306;Database=109875;Uid=109875;Pwd=SprOmyro;";

    //index pagina functie decleration
    public IActionResult Index()
    {
      List<Festival> festivals = new List<Festival>();
      festivals = GetFestivals();
      return View(festivals);
    }





    //Functie Getfestival om data uit festival tabel op te vragen
    private List<Festival> GetFestivals()
    {
      List<Festival> festivals = new List<Festival>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand($"select * from festival ", conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {
            Festival p = new Festival
            {
              Id = Convert.ToInt32(reader["ID"]),
              Naam = reader["Titel"].ToString(),
              Beschrijving = reader["Beschrijving"].ToString(),
              Headliners = reader["Headliners"].ToString(),
              Prijs = Convert.ToDecimal(reader["Prijs"]),
              Plaatje = reader["Plaatje"].ToString(),
              Minimum_leeftijd = reader["Minimum_leeftijd"].ToString(),
              Datum = reader["Datum"].ToString(),


            };
            festivals.Add(p);
          }
        }
      }
      return festivals;
    }


    //HuisrgelsFAQ functie en model declaration
    [Route("HuisregelsFAQ")]
    public IActionResult HuisregelsFAQ()
    {
      List<Huisregel> huisregels = new List<Huisregel>();
      huisregels = GetHuisregels();

      List<FAQ> faqs = new List<FAQ>();
      faqs = Getfaqs();


      var huisregelsFAQViewModel = new HuisregelsFAQViewModel
      {
        huisregels = huisregels,
        faqs = faqs,

      };
      return View(huisregelsFAQViewModel);
    }


    //Functie, haalt data uit huisregel tabel
    private List<Huisregel> GetHuisregels()
    {
      List<Huisregel> huisregels = new List<Huisregel>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();

        MySqlCommand huisregelscmd = new MySqlCommand($"select * from huisregel ", conn);
        using (var huisregelreader = huisregelscmd.ExecuteReader())
        {
          while (huisregelreader.Read())
          {
            Huisregel huisregel = new Huisregel
            {
              HuisregelId = huisregelreader["HuisregelId"].ToString(),
              Regel = huisregelreader["Regel"].ToString(),
              Toelichting = huisregelreader["Toelichting"].ToString(),
            };
            huisregels.Add(huisregel);
          }
        }
        return huisregels;
      }
    }

    //Functie, haalt data uit FAQ tabel
    private List<FAQ> Getfaqs()
    {
      List<FAQ> faqs = new List<FAQ>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();

        MySqlCommand faqscmd = new MySqlCommand($"select * from faq ", conn);
        using (var faqreader = faqscmd.ExecuteReader())
        {
          while (faqreader.Read())
          {
            FAQ faq = new FAQ
            {
              FAQId = faqreader["FAQId"].ToString(),
              FAQtekst = faqreader["FAQ"].ToString(),
              antwoord = faqreader["antwoord"].ToString(),
            };
            faqs.Add(faq);
          }
        }
        return faqs;
      }
    }


    //Route naar Transport pagina
    [Route("Transport")]
    public IActionResult Transport()
    {
      return View();
    }

    //Route naar kaarten pagina + declaration
    [Route("Kaarten/{id}")]
    public IActionResult Kaarten(string id)
    {
      var model = GetFestival(id);
      return View(model);
    }

    //Route naar contact pagina
    [Route("contact")]
    public IActionResult Contact()
    {
      return View();
    }

    //Functie die het opslaan van contactformulier aanroept als hij valid is
    [Route("contact")]
    [HttpPost]
    public IActionResult Contact(Personmodel model)
    {
      if (!ModelState.IsValid)
        return View(model);

      SavePerson(model);

      ViewData["formsucces"] = "ök";

      return View();
    }

    //opslaan contactformulier in database
    private void SavePerson(Personmodel person)
    {
      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam, achternaam, emailadres, bericht) VALUES(?voornaam, ?achternaam,?email,?bericht)", conn);
        cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
        cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
        cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.Email;
        cmd.Parameters.Add("?bericht", MySqlDbType.VarChar).Value = person.Bericht;
        cmd.ExecuteNonQuery();
      }
    }
    [Route("show-all")]


    public IActionResult ShowAll()
    {
      return View();
    }

    //Individuele festivalpagina route en aangeven gebruik Getfestival functie
    [Route("festivals/{id}")]
    public IActionResult Festival(string id)
    {
      var model = GetFestival(id);
      return View(model);
    }

    //Functie Getfestival om data uit  database tabellen op te vragen
    private Festival GetFestival(string id)
    {
      //aanmaak lijst van festival data
      List<Festival> festivals = new List<Festival>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
        //aanmaak lijst prijzen data in de festival lijst
        MySqlCommand prijzencmd = new MySqlCommand($"select * from prijzen where PrijsId = {id}", conn);
        List<Prijs> prijzen = new List<Prijs>();

        using (var prijzenreader = prijzencmd.ExecuteReader())
        {
          while (prijzenreader.Read())
          {
            Prijs prijs = new Prijs
            {
              Dag1 = prijzenreader["Dag1"].ToString(),
              Dag2 = prijzenreader["Dag2"].ToString(),
              Dag3 = prijzenreader["Dag3"].ToString(),
              WeekendExcl = prijzenreader["WeekendExcl"].ToString(),
              WeekendIncl = prijzenreader["WeekendIncl"].ToString(),
            };
            prijzen.Add(prijs);
          }
        }
        //aanmaak lijst artiesten data in de festival lijst

        MySqlCommand artiestencmd = new MySqlCommand($"select * from artiesten where FestivalId = {id}", conn);
        List<Artiest> artiesten = new List<Artiest>();

        using (var artiestenreader = artiestencmd.ExecuteReader())
        {
          while (artiestenreader.Read())
          {
            Artiest artiest = new Artiest
            {
              Dag1Act = artiestenreader["Dag1Act"].ToString(),
              Dag2Act = artiestenreader["Dag2Act"].ToString(),
              Dag3Act = artiestenreader["Dag3Act"].ToString(),

            };
            artiesten.Add(artiest);
          }
        }
        //prijzen en artiesten pagina toevoegen aan festivallijst
        MySqlCommand cmd = new MySqlCommand($"select * from festival where id = {id}", conn);

        using (var reader = cmd.ExecuteReader())
        {
          while (reader.Read())
          {

            Festival p = new Festival
            {
              Id = Convert.ToInt32(reader["Id"]),
              Naam = reader["Titel"].ToString(),
              Beschrijving = reader["Beschrijving"].ToString(),
              Artiesten = artiesten,
              Plaatje = reader["Plaatje"].ToString(),
              Prijzen = prijzen,
            };
            festivals.Add(p);
          }
        }
      }
      return festivals[0];
    }
  }
}



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
    // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
    string connectionString = "Server=informatica.st-maartenscollege.nl ;Port=3306;Database=109875;Uid=109875;Pwd=SprOmyro;";

        public IActionResult Index()
    {
      List<Festival> festivals = new List<Festival>();
      // regel hierondor commenten om database uit te zettenf
      festivals = GetFestivals();
      return View(festivals); 
    }



    public IActionResult Privacy()
    {
      return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }


    // public IActionResult ShowAll()
    //{
    //  return View();
    //}

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
              Minimum_leeftijd = Convert.ToInt32(reader["Minimum_leeftijd"]),
            };
            festivals.Add(p);
          }
        }
      }
      return festivals;
    }

    [Route("HuisregelsFAQ")]
    public IActionResult HuisregelsFAQ()
    {
      return View();
    }

    [Route("Transport")]
    public IActionResult Transport()
    {
      return View();
    }


    [Route("contact")]
    public IActionResult Contact()
    {
      return View();
    }

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

    [Route("festivals/{id}")]
    public IActionResult Festival(string id)
    {
      var model = GetFestival(id);
      ViewData["artiesten"] = GetFestivals();
      ViewData["prijzen"] = GetFestivals();


      return View(model);
    }

    private Festival GetFestival(string id)
    {
      List<Festival> festivals = new List<Festival>();

      using (MySqlConnection conn = new MySqlConnection(connectionString))
      {
        conn.Open();
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

            };
            prijzen.Add(prijs);
          }
        }

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
              Plaatje  = reader["Plaatje"].ToString(),
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



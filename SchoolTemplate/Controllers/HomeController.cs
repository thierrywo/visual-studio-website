using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Database;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
    public class HomeController : Controller
    {
        // zorg ervoor dat je hier je gebruikersnaam (leerlingnummer) en wachtwoord invult
        string connectionString = "Server=172.16.160.21;Port=3306;Database=109875;Uid=109875;Pwd=SprOmyro;";

        public IActionResult Index()
        {
            List<Product> festivals = new List<Product>();
            // uncomment deze regel om producten uit je database toe te voegen

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


        public IActionResult ShowAll()
        {
            return View();
        }

    [Route("festivals/{id}")]
public IActionResult Festival(string id)
        {
            var model = GetFestival(id);
            return View(model);
        }

        private Festival GetFestivals(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from product where id = {id}", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString()
                        };
                        festivals.Add(p);
                    }
                }
            }
            return festivals[0];
        }

    }
}

private Festival Getfestival(string id)
{
    List<Festival> festivals = new List<Festival>();

    using (MySqlConnection conn = new MySqlConnection(connectionString)
    {
        conn.Open();
    MySqlCommand cmd = new MySqlCommand($"select * from product where id= {id}", conn);

    using (var reader = cmd.ExecuteReader())
    {
        while (reader.Read())
        {
            festival p = new festival
            {
                Ìd = Convert.ToInt32(reader["id"]),
                Naam = reader["Naam"].ToString()
            }
        festivals.Add(p);
        }
    }
    }
}
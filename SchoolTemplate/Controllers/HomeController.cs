﻿using System;
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


        public IActionResult ShowAll()
        {
            return View();
        }

        private List<Festival> GetFestivals(string id)
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["ID"]),
                            Naam = reader["Naam"].ToString(),
                            Beschrijving = reader["beschrijving"].ToString()
                            Headliners = reader["Headliners"].ToString()
                            Prijs = reader["Prijs"].ToString()
                            //Plaatje =
                            Minimum_leeftijd = reader["Prijs"].ToString()
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
                MySqlCommand cmd = new MySqlCommand("INSERT INTO klant(naam, achternaam, emailadres) VALUES(?voornaam, ?achternaam,?email)", conn);
                cmd.Parameters.Add("?voornaam", MySqlDbType.VarChar).Value = person.Voornaam;
                cmd.Parameters.Add("?achternaam", MySqlDbType.VarChar).Value = person.Achternaam;
                cmd.Parameters.Add("?email", MySqlDbType.VarChar).Value = person.Email;
                cmd.ExecuteNonQuery();
            }
        }
        private List<Festival> GetFestivals()
        {
            List<Festival> festivals = new List<Festival>();

            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand($"select * from festival", conn);

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Festival p = new Festival
                        {
                            Id = Convert.ToInt32(reader["id"]),
                            Naam = reader["Titel"].ToString()
                        };
                        festivals.Add(p);
                    }
                }
                return festivals;
            }
        }
    }
}



using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using SchoolTemplate.Models;

namespace SchoolTemplate.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      //string connectionString = "Server=172.16.160.21;Port=3306;Database=;Uid=;Pwd=;";
   
      //using (MySqlConnection conn = new MySqlConnection(connectionString))
      //{
      //  conn.Open();
      //  MySqlCommand cmd = new MySqlCommand("select * from product", conn);

      //  using (var reader = cmd.ExecuteReader())
      //  {
      //    while (reader.Read())
      //    {

      //      int Id = Convert.ToInt32(reader["Id"]);
      //      string Name = reader["Naam"].ToString();
      //    }
      //  }

      //}

      return View();
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
  }
}

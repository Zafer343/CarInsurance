using CarInsurance.Models;
using CarInsurance.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CarInsurance.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendForm(string firstName, string lastName, string emailAddress, DateTime dateOfBirth, int carYear, string carMake, string carModel, bool dui, int speedTickets, string coverage)
        {
            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(emailAddress) ||
                dateOfBirth == DateTime.MinValue || carYear == 0 || string.IsNullOrEmpty(carMake) ||
                string.IsNullOrEmpty(carModel) || string.IsNullOrEmpty(coverage))
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            decimal quoteAmount = CalculateQuote(dateOfBirth, carYear, carMake, carModel, dui, speedTickets, coverage);

            using (var db = new CarInsuranceEntities1())
            {
                var quote = new InsuranceQuote
                {
                    FirstName = firstName,
                    LastName = lastName,
                    EmailAddress = emailAddress,
                    DateOfBirth = dateOfBirth,
                    CarYear = new DateTime(carYear, 1, 1),
                    CarMake = carMake,
                    CarModel = carModel,
                    Dui = dui,
                    SpeedTickets = speedTickets,
                    FullCoverage = coverage == "Full",
                    Quote = quoteAmount
                };

                db.InsuranceQuotes.Add(quote);
                db.SaveChanges();
            }

            return View("Quote", new QuoteVm { QuoteAmount = quoteAmount });
        }

        private decimal CalculateQuote(DateTime dateOfBirth, int carYear, string carMake, string carModel, bool dui, int speedTickets, string coverage)
        {
            decimal quoteAmount = 50;
            int currentYear = DateTime.Now.Year;
            int userAge = currentYear - dateOfBirth.Year;

            // Yaş kontrolü
            if (dateOfBirth > DateTime.Now.AddYears(-userAge)) userAge--;

            if (userAge < 18)
                quoteAmount += 100;
            else if (userAge > 18 && userAge < 26)
                quoteAmount += 50;
            else if (userAge < 25 || userAge > 100) 
                quoteAmount += 25;

            // Araç yılı kontrolü
            if (carYear < 2000 || carYear > 2015)
                quoteAmount += 25;

            // Marka ve model kontrolü
            if (carMake.ToLower() == "porsche")
            {
                quoteAmount += 25;
                if (carModel.ToLower() == "911 carrera")
                    quoteAmount += 25;
            }

            // Ceza puanı hesaplama
            quoteAmount += speedTickets * 10;

            // DUI kontrolü
            if (dui)
                quoteAmount *= 1.25m;

            // Kapsam kontrolü
            if (coverage == "Full")
                quoteAmount *= 1.5m;

            return quoteAmount;
        }
    }
}
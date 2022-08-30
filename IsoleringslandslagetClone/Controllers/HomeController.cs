using IsoleringslandslagetClone.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace IsoleringslandslagetClone.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Members()
        {
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
        [HttpPost]
        public IActionResult GetInfo(Contact contact)
        {
            if(contact != null)
            {
                Console.WriteLine(contact.name);
                SendMail(contact).Wait();
                return Redirect("/home");
            }
            return Redirect("/home");
        }

        static async Task SendMail(Contact contact)
        {
            string newLine = Environment.NewLine;

            var apiKey = "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";
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(contact.email, contact.name);
            var subject = $"Isolerings förfrågan i {contact.city} ";
            var to = new EmailAddress("william@isoleringslandslaget.se", "William");

            var plainTextContent = $"NAMN: {contact.name} {newLine} ORT: {contact.city} {newLine} TELEFONNUMMER: {contact.phoneNumber}" +
                $"EMAIL: {contact.email} {newLine} MEDDELANDE: {contact.message}";

            var htmlContent = $"NAMN: {contact.name} {newLine} ORT: {contact.city} {newLine} TELEFONNUMMER: {contact.phoneNumber}" +
                $"EMAIL: {contact.email} {newLine} MEDDELANDE: {contact.message}";

            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var response = await client.SendEmailAsync(msg);
        }
    }
}
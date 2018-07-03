using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace GTMetrix.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewData["Message"] = "";
            return View();
        }

        /// <summary>
        /// Send the user an HTML+ text confirmation e-mail that contains all the information they submitted
        /// The e-mail is generated from a separate template(so it would be easily editable by a user)
        /// </summary>
        /// <param name="reg"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Index(Reg reg)
        {
            string body = PopulateBody(reg.Name, reg.Email, reg.OptionReferrer, reg.AccountType, reg.Coupon);
            

            if(SendHtmlFormattedEmail(reg.Email, body))
                ViewData["Message"] = "✔ Thanks for signing up, " + reg.Name + "!";
            else
                ViewData["Message"] = "signing up,failed!";
            return View();
        }

        /// <summary>
        /// Returns a JSON reply with whether or not the code is valid or not and a success/error message
        /// </summary>
        /// <param name="CouponString"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult AddCoupon(string CouponString)
        {
            List <string> AllowedCoupons = new List<string>() { "A12", "B22", "C33" };
            bool exists = false;
            if (CouponString != null)
            {
                exists = AllowedCoupons.Any(s => s.Equals(CouponString, StringComparison.OrdinalIgnoreCase));
            }

            if (exists)
                return Json(new { success = true, responseText = "Your coupon is valid!" }, JsonRequestBehavior.AllowGet);
            else
                return Json(new { success = false, responseText = "Your coupon is not valid." }, JsonRequestBehavior.AllowGet);
        }

        private bool SendHtmlFormattedEmail(string email, string body)
        {
            try
            {
                using (MailMessage mailMessage = new MailMessage())

                {

                    mailMessage.From = new MailAddress(System.Configuration.ConfigurationManager.AppSettings["UserName"]);

                    mailMessage.Subject = "";

                    mailMessage.Body = body;

                    mailMessage.IsBodyHtml = true;

                    mailMessage.To.Add(new MailAddress(email));

                    SmtpClient smtp = new SmtpClient();

                    smtp.Host = ConfigurationManager.AppSettings["Host"];

                    smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSsl"]);

                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();

                    NetworkCred.UserName = ConfigurationManager.AppSettings["UserName"]; //reading from web.config  

                    NetworkCred.Password = ConfigurationManager.AppSettings["Password"]; //reading from web.config  

                    smtp.UseDefaultCredentials = true;

                    smtp.Credentials = NetworkCred;

                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]); //reading from web.config  

                    smtp.Send(mailMessage);

                    return true;
                }
            }
            catch(Exception ex)
            {
                throw ex;
                return false;
            }

        }

        private string PopulateBody(string Name, string Email, List<string> optionReferrer, string AccountType, string Coupon)
        {
            string body = string.Empty;
            //string emailHTMLpath = Server.MapPath("~/View/Home/EmailTemplate.cshtml");
            string emailHTMLpath = "~/View/Home/EmailTemplate.cshtml";
            
            using (StreamReader reader = new StreamReader(@emailHTMLpath)) 
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{Name}", Name);
            body = body.Replace("{Email}", Email);

            string joinedReferrer = string.Join(",", optionReferrer);

            body = body.Replace("{optionReferrer}", joinedReferrer);
            body = body.Replace("{AccountType}", AccountType);
            body = body.Replace("{Coupon}", Coupon);
            return body;
        }


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }

    public class Reg
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<string> OptionReferrer { get; set; }
        public string AccountType { get; set; }
        public string Coupon { get; set; }
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Umbraco.Web.Mvc;
using Umbraco7.Model;
using System.IO;
using umbraco.editorControls.SettingControls.Pickers;
using System.Configuration;

namespace Umbraco7.Controllers
{
    public class OrderConfirmationController : SurfaceController
    {
        // GET: OrderConfirmation
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrderConfirmation(InfomationMail infomationMail)
        {
            string Mailtemplate = System.IO.File.ReadAllText(System.AppContext.BaseDirectory+"/Utils/Template/mailtemplate.html");
            Mailtemplate=Mailtemplate.Replace("{hoTen}", infomationMail.nameCustomer);
            Mailtemplate= Mailtemplate.Replace("{Email}", infomationMail.Email);
            Mailtemplate= Mailtemplate.Replace("{diaChi}", infomationMail.Address);
            if(SendMail("Thông tin khách hàng", Mailtemplate,infomationMail.Email))
            {
                return Redirect("~/en");
            }
            return Redirect("~/vi");
        }

        private bool SendMail(string Subject, string htmlString,string email)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress("support@3steam.net");
                message.To.Add(new MailAddress(email));
                message.Subject = Subject;
                message.IsBodyHtml = true; //to make message body as html  
                message.Body = htmlString;
                smtp.Port = 587;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //smtp.Host = "smtp.gmail.com";//for gmail host  
                smtp.Host = "smtp.office365.com"; //for office 365 host
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("support@3steam.net", ConfigurationManager.AppSettings["passEmail"]);
                smtp.Send(message);
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
    }
}
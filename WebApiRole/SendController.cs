using System;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Results;
using NExtensions;

namespace WebApiRole
{
    public class SendController : ApiController
    {
        //gmail account settings: 
        private const string From = "simplewebapimailer@gmail.com";
        private const string Password = "S1mpl3w3bap1M@1l3r";

        [HttpPost]
        [Route("api/send")]
        public IHttpActionResult SendEmail(Email email)
        {
            Console.WriteLine("Received email send request for: ".Append(email == null ? "<NULL>" : email.ToString()));

            var body = "Sent from: ".Append(email.From).Append(Environment.NewLine).Append(email.Body);

            var msg = new MailMessage(From, email.To) {Subject = email.Subject, Body = body};
            using (var smtp = new SmtpClient())
            {
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new NetworkCredential(From, Password);

                try
                {
                    smtp.Send(msg);
                }
                catch (Exception ex)
                {
                    var exmsg = ex.GetBaseException().GetType().Name + " - " + ex.GetBaseException().Message;
                    Console.WriteLine("ERROR: ".Append(exmsg));
                    return new ResponseMessageResult(new HttpResponseMessage(HttpStatusCode.InternalServerError) { Content = new StringContent(exmsg) });
                }
            }

            return Ok();
        }
    }

    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }

        public override string ToString()
        {
            return new[]{From, To, Subject}.JoinWithComma(StringJoinOptions.AddSpaceSuffix);
        }
    }
}
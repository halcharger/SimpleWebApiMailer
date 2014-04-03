using System;
using System.Net.Http;
using System.Web.Http;
using NExtensions;

namespace WebApiRole
{
    public class SendController : ApiController
    {
        [HttpPost]
        [Route("api/send")]
        public IHttpActionResult SendEmail(Email email)
        {
            Console.WriteLine("Received email send request for: ".Append(email == null ? "<NULL>" : email.ToString()));
            return Ok(new StringContent("boom!!!"));
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
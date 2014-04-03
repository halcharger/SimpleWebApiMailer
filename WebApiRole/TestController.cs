using System.Net.Http;
using System.Web.Http;

namespace WebApiRole
{
    public class TestController : ApiController
    {
        [HttpGet]
        [Route("api/send")]
        public IHttpActionResult SendEmail()
        {
            return Ok(new StringContent("boom!!!"));
        }
    }

    public class Email
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public string From { get; set; }
        public string To { get; set; }
    }
}
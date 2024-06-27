using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace simpleWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : Controller
    {
        // GET: /HelloWorld/
        [HttpGet]
        public string Index()
        {
            return "This is my default action...";
        }

        // GET: /HelloWorld/Welcome?name=Hohn&age=90
        [HttpGet("Welcome")]
        public string Welcome(string name, int age)
        {
            return $"Welcome, {name}, {age}!";
        }
    }
}

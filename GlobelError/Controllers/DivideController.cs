using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlobelError.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DivideController : ControllerBase
    {
       [HttpGet]
       public IActionResult Get() 
       {
            //Console.Write("Enter the First number= ");
            //int a = int.Parse(Console.ReadLine());

            //Console.Write("Enter the second no= ");
            //int b = int.Parse(Console.ReadLine());

            //int c = a / b;
            //Console.WriteLine("Ans= " + c);

            int a = 5;
            int b = a / 0;
            return Ok(b);
       }
        [HttpPost]
        public IActionResult Post(int b)
        {
            int a = 10;
            int c = a / b;
            return Ok(c);
        }
    }
}

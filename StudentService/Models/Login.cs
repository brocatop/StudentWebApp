using System.ComponentModel.DataAnnotations;
using System.Net.Http;

namespace StudentService.Models
{
    public class Login
    {
    }

    public class LoginRequest
    {
        [Key]
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public LoginResponse()
        {

            this.Token = "";
            this.ResponseMsg = new HttpResponseMessage() { StatusCode = System.Net.HttpStatusCode.Unauthorized };
        }

        public string Token { get; set; }
        public HttpResponseMessage ResponseMsg { get; set; }

    }
}
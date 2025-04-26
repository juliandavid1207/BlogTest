using BlogsWebApi.Models.DTO;

namespace BlogsWebApi.Models.Returns
{
    public class Response
    {
        public bool created { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}

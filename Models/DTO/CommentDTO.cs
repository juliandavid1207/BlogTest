namespace BlogsWebApi.Models.DTO
{
    public class CommentDTO
    {
        public string Comment { get; set; }
        public int IdPost { get; set; }
        public int IdUser { get; set; }
    }
}

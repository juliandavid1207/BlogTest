namespace BlogsWebApi.Models.Returns
{
    public class PostResponse
    {
        public string Tipo { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public List<CommentsResponse> commments { get; set; }
    }
}

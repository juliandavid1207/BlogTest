using BlogsWebApi.Models;
using BlogsWebApi.Models.DTO;
using BlogsWebApi.Models.Returns;
using BlogsWebApi.Security;
using System.Xml.Linq;

namespace BlogsWebApi.Services
{
    public static class Mappers
    {
        public static User GetUser(UserDTO userDTO)
        {
            var user = new User
            {
                Name = userDTO.Name,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = UserValidator.GetHash(userDTO.Password)
            };

            return user;
        }

        public static Post GetPost(PostDTO postDTO)
        {
            var post = new Post
            {
                PostType = postDTO.PostType,
                PostPath = postDTO.PostPath,
                IdUser = postDTO.IdUser,
            };

            return post;
        }

        public static Comment GetComment(CommentDTO commentDTO)
        {
            var comment = new Comment
            {
                Comment1 = commentDTO.Comment,
                IdPost = commentDTO.IdPost,
                IdUser = commentDTO.IdUser,
            };

            return comment;
        }

        public static List<CommentsResponse> GetCommentsUser(List<Comment> comments)
        {
            var commentsPost= new List<CommentsResponse>();
            foreach (var comment in comments)
            {
                var commentX = new CommentsResponse();
                commentX.comment = comment.Comment1;
                commentX.userName = comment.IdUserNavigation.Name;
                commentX.userLastName = comment.IdUserNavigation.LastName;
                commentsPost.Add(commentX);
            }

            return commentsPost;
        }

        public static PostResponse GetPostUser(Post post)
        {
            var postResponse = new PostResponse();
            postResponse.Tipo = post.PostTypeNavigation.Type;
            postResponse.userName = post.IdUserNavigation.Name;
            postResponse.userLastName = post.IdUserNavigation.LastName;
            var commentsPost = new List<CommentsResponse>();
            foreach (var comment in post.Comments)
            {
                var commentX = new CommentsResponse();
                commentX.comment = comment.Comment1;
                commentX.userName = comment.IdUserNavigation.Name;
                commentX.userLastName = comment.IdUserNavigation.LastName;
                commentsPost.Add(commentX);
            }
            postResponse.commments = commentsPost;
            return postResponse;
        }
    }
}

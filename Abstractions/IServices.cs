using BlogsWebApi.Models;
using BlogsWebApi.Models.DTO;
using BlogsWebApi.Models.Returns;
using Microsoft.AspNetCore.Mvc;

namespace BlogsWebApi.Abstractions
{
    public interface IServices
    {
        public Task<Response> AddComment(CommentDTO commentDTO);
        public Task<Response> AddPost(PostDTO postDTO);
        public Task<User> AddUser(User user);
        public Task<UserAuth> ValidateUser(string user, string password);
        public Task<Response> GetComments(int idPost);
        public Task<Response> GetPost(int idPost);
        public Task<Response> UpdateComment(int idComment, CommentDTO commentDTO);
        public Task<Response> UpdatePost(int idPost, PostDTO postDTO);
        public Task<Response> DeleteComment(int idComment);
        public Task<Response> DeletePost(int idPost);
    }
}

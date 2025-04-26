using BlogsWebApi.Abstractions;
using BlogsWebApi.Models;
using BlogsWebApi.Models.DTO;
using BlogsWebApi.Models.Returns;
using BlogsWebApi.Security;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace BlogsWebApi.Services
{
    public class Services : IServices
    {
        private readonly BlogWA2Context _dbContext;
        public Services(BlogWA2Context dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Response> AddComment(CommentDTO commentDTO)
        {
            var response = new Response
            {
                created = false,
                data = commentDTO,
                message = string.Empty
            };

            try
            {
                var post = _dbContext.Posts.FirstOrDefault(p => p.IdPost == commentDTO.IdPost);
                var user = _dbContext.Users.FirstOrDefault(u => u.IdUser == commentDTO.IdUser);

                if (post == null)
                {
                    response.message = "Post no encontrado";
                    return response;
                }

                if (user == null)
                {
                    response.message = "Usuario no encontrado";
                    return response;
                }

                var comment = Mappers.GetComment(commentDTO);

                comment.IdPostNavigation = post;


                await _dbContext.Comments.AddAsync(comment);
                await _dbContext.SaveChangesAsync();
                response.created = true;
                response.message = "Ok";
                return response;
            }
            catch (Exception ex)
            {
                response.message = $"Error en el servidor:{ex.Message}";
                return response;
            }

        }

        public async Task<Response> AddPost(PostDTO postDTO)
        {
            var response = new Response
            {
                created = false,
                data = postDTO,
                message = string.Empty
            };

            try
            {
                var user = _dbContext.Users.FirstOrDefault(u => u.IdUser == postDTO.IdUser);
                var type = _dbContext.PostTypes.FirstOrDefault(pt => pt.IdPostType == postDTO.PostType);
                if (user == null)
                {
                    response.message = "Usuario no encontrado";
                    return response;
                }

                if (type == null)
                {
                    response.message = "Tipo no encontrado";
                    return response;
                }

                var post = Mappers.GetPost(postDTO);

                post.IdUserNavigation = user;
                post.PostTypeNavigation = type;


                await _dbContext.Posts.AddAsync(post);
                await _dbContext.SaveChangesAsync();
                response.created = true;
                response.message = "Ok";
                return response;
            }
            catch (Exception ex)
            {
                response.message = $"Error en el servidor:{ex.Message}";
                return response;
            }
        }

        public async Task<User> AddUser(User user)
        {
            try
            {
                await _dbContext.Users.AddAsync(user);
                await _dbContext.SaveChangesAsync();

                return user;
            }
            catch (Exception ex)
            {

                return null;

            }
        }

        public async Task<Response> DeleteComment(int idComment)
        {
            var response = new Response
            {
                created = false,
                data = null,
                message = string.Empty
            };
            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(c=>c.IdComment == idComment);

                if (comment == null)
                {
                    response.message = "Comentario no encontrado";
                    return response;
                }


                _dbContext.Comments.Remove(comment);
                await _dbContext.SaveChangesAsync();
                response.created = true;
                return response;

            }
            catch(Exception ex)
            {
                response.message = $"Error en el servidor: {ex.Message}";
                return response;
            }
        }

        public async Task<Response> DeletePost(int idPost)
        {
            var response = new Response
            {
                created = false,
                data = null,
                message = string.Empty
            };
            try
            {
                var post = await _dbContext.Posts.Where(p => p.IdPost == idPost)
                    .Include(p=>p.Comments).ToListAsync();

                if (post == null)
                {
                    response.message = "Post no encontrado";
                    return response;
                }


                _dbContext.Posts.RemoveRange(post);
                await _dbContext.SaveChangesAsync();
                response.created = true;
                return response;

            }
            catch (Exception ex)
            {
                response.message = $"Error en el servidor: {ex.Message}";
                return response;
            }
        }

        public async Task<Response> GetComments(int idPost)
        {
            var response = new Response
            {
                created = false,
                data = null,
                message = string.Empty
            };

            try
            {
                var comments = await _dbContext.Comments.Where(c => c.IdPost == idPost)
                    .Include(c=>c.IdUserNavigation)
                    .Include(c=>c.IdPostNavigation)              
                    .ToListAsync();

                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.IdPost == idPost);

                if (post == null)
                {
                    response.message = "No se encontro el post";
                    return response;
                }

                if (comments.Count == 0 || comments == null)
                {
                    response.message = "No se encontraron comentarios";
                    return response;
                }
              
                var commentsResponse = Mappers.GetCommentsUser(comments);

                response.created = true;
                response.data = commentsResponse;
                return response;
            }
            catch (Exception ex)
            {
                response.message = $"Error en el servidor:{ex.Message}";
                return response;
            }
        }

        public async Task<Response> GetPost(int idPost)
        {
            var response = new Response
            {
                created = false,
                data = null,
                message = string.Empty
            };

            try
            {
                var comments = await _dbContext.Posts.Where(c => c.IdPost == idPost)
                    .Include(c => c.PostTypeNavigation)
                    .Include(c => c.IdUserNavigation)
                    .Include(c=>c.Comments)
                    .ToListAsync();

                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.IdPost == idPost);

                if (post == null)
                {
                    response.message = "No se encontro el post";
                    return response;
                }              

                var postResponse = Mappers.GetPostUser(post);

                response.created = true;
                response.data = postResponse;
                return response;
            }
            catch (Exception ex)
            {
                response.message = $"Error en el servidor:{ex.Message}";
                return response;
            }
        }

        public async Task<Response> UpdateComment(int idComment, CommentDTO commentDTO)
        {
            var response = new Response
            {
                created = false,
                data = null,
                message = string.Empty
            };
            try
            {
                var comment = await _dbContext.Comments.FirstOrDefaultAsync(c => c.IdComment == idComment);
                if(comment == null)
                {
                    response.message = "No se encontró el comentario";
                    return response;
                }

                comment.Comment1 = commentDTO.Comment;
                _dbContext.Comments.Update(comment);
                _dbContext.SaveChanges();

                response.created= true;
                response.data = commentDTO;
                return response;
            }
            catch(Exception ex)
            {
                response.message = $"Error en el servidor:{ex.Message}";
                return response;
            }
        }

        public async Task<Response> UpdatePost(int idPost, PostDTO postDTO)
        {
            var response = new Response
            {
                created = false,
                data = null,
                message = string.Empty
            };
            try
            {
                var post = await _dbContext.Posts.FirstOrDefaultAsync(p => p.IdPost == idPost);
                if (post == null)
                {
                    response.message = "No se encontró el post";
                    return response;
                }

                post.PostPath = postDTO.PostPath;
                post.PostType = postDTO.PostType;
                _dbContext.Posts.Update(post);
                _dbContext.SaveChanges();

                response.created = true;
                response.data = postDTO;
                return response;
            }
            catch (Exception ex)
            {
                response.message = $"Error en el servidor:{ex.Message}";
                return response;
            }
        }

        public async Task<UserAuth> ValidateUser(string email, string password)
        {
            var userAuth = new UserAuth
            {
                autenticated = false
            };

            try
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                    return userAuth;

                var result = UserValidator.VerifyHash(user, password);

                if (result == false)
                    return userAuth;

                userAuth.user = user;
                userAuth.autenticated = true;

                return userAuth;
            }
            catch (Exception ex)
            {
                return userAuth;
            }
        }
    }
}

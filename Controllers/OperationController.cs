using BlogsWebApi.Abstractions;
using BlogsWebApi.Models;
using BlogsWebApi.Models.DTO;
using BlogsWebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogsWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class OperationController : ControllerBase
    {
        private readonly IServices _services;
        public OperationController(IServices services)
        {
            _services = services;
        }

        [HttpPost("InsertComments")]
        public async Task<IActionResult> InsertComments([FromBody] CommentDTO commentDTO)
        {
            var result = await _services.AddComment(commentDTO);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpPost("InsertPosts")]
        public async Task<IActionResult> InsertPost([FromBody] PostDTO postDTO)
        {
            var result = await _services.AddPost(postDTO);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpGet("GetComments")]
        public async Task<IActionResult> GetComments(int idPost)
        {
            var result = await _services.GetComments(idPost);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpGet("GetPost")]
        public async Task<IActionResult> GetPost(int idPost)
        {
            var result = await _services.GetPost(idPost);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(int idPost, PostDTO postDTO)
        {
            var result = await _services.UpdatePost(idPost,postDTO);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(int idComment, CommentDTO commentDTO)
        {
            var result = await _services.UpdateComment(idComment, commentDTO);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpDelete("DeleteComment")]
        public async Task<IActionResult> DeleteComment(int idComment)
        {
            var result = await _services.DeleteComment(idComment);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }

        [HttpDelete("DeletePost")]
        public async Task<IActionResult> DeletePost(int idPost)
        {
            var result = await _services.DeletePost(idPost);
            if (result.created)
            {
                return Ok(result.data);
            }

            return BadRequest(result.message);
        }
    }
}

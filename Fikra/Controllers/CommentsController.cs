using Fikra.DAL.Modules.Comments;
using Fikra.DAL.Modules.Comments.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Fikra.Controllers
{
    [Route("comments")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        #region Constructor
        private readonly ICommentsRepository commentsRepository;

        public CommentsController(ICommentsRepository commentsRepository)
        {
            this.commentsRepository = commentsRepository;
        }
        #endregion

        #region Actions
        [AllowAnonymous]
        [HttpGet("get-comments/{PostID}")]
        public async Task<ActionResult<IEnumerable<CommentsGetByPostIDDto>>> CommentsGetByPostID(int PostID)
        {
            var result = await commentsRepository.CommentsGetByPostID(PostID);
            return Ok(result);
        }

        [HttpDelete("Delete/{CommentID}")]
        public async Task<ActionResult> CommentDeleteByID(int CommentID)
        {
            var UserID = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);

            var UserRole = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (UserRole == "Admin")
            {
                await commentsRepository.CommentDeleteByID(CommentID);
                return Ok();
            }
            else
            {
                var comment = await commentsRepository.CommentsGetByID(CommentID);
                if (UserID == comment.CommentUserID)
                {
                    await commentsRepository.CommentDeleteByID(CommentID);
                    return Ok();
                }
                else
                {
                    return Unauthorized();
                }
            }
        }

        [HttpPost("Create")]
        public async Task<ActionResult> CommentCreate(CommentCreateDto commentCreateDto)
        {
            await commentsRepository.CommentCreate(commentCreateDto);
            return Ok();
        }

        [HttpPut("Update")]
        public async Task<ActionResult> CommentUpdate(CommentUpdateDto commentUpdateDto)
        {
            var UserID = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "UserID")?.Value);
            var Comment = await commentsRepository.CommentsGetByID(commentUpdateDto.CommentID);

            if (UserID == Comment.CommentUserID)
            {
                await commentsRepository.CommentUpdate(commentUpdateDto);
                return Ok();
            }
            return Unauthorized();
        }
        #endregion
    }
}

using Fikra.Constants;
using Fikra.DAL.Modules.Posts;
using Fikra.DAL.Modules.Posts.PostDTO;
using FikraModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using static Fikra.Filters.ClaimAuthorizationFilter;

namespace Fikra.Controllers
{
    [Route("posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        #region Constructor
        private readonly IPostsRepository postsRepository;

        public PostsController(IPostsRepository postsRepository)
        {
            this.postsRepository = postsRepository;
        }
        #endregion

        #region Actions
        [HttpGet("get-all")]
        public async Task<ActionResult<List<PostsGetAllDTO>>> GetAllPostsAsync()
        {
            var Role = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            if (Role == RoleNames.Admin)
            {
                var result = await postsRepository.PostsGetAll(forAdmin: true);
                return Ok(result);
            }
            else
            {
                var result = await postsRepository.PostsGetAll(forAdmin: false);
                return Ok(result);
            }
        }

        [HttpGet("{PostID:int}")]
        public async Task<ActionResult<PostsGetByIDDTO>> GetPostByIDAsync(int PostID)
        {
            var result = await postsRepository.PostsGetByID(PostID);
            return Ok(result);
        }

        [HttpPost("create-anonymouse")]
        public async Task<ActionResult<List<Post>>> PostCreateAnonymous(string anonymousName, IFormFile file)
        {
            await postsRepository.PostCreateAnonymous(anonymousName, file);
            return Ok();
        }

        [Authorize]
        [HttpPost("create-authorised")]
        [RequireClaim("PostCreateAuthorised")]
        public async Task<ActionResult> PostCreateAuthorised(PostsCreateDTO post)
        {
            await postsRepository.PostsCreate(post);
            return Ok();
        }

        [HttpDelete("{PostID}")]
        [RequireClaim("DeletePostByIDAsync")]

        public async Task<ActionResult> DeletePostByIDAsync(int PostID)
        {
            await postsRepository.PostsDeleteByID(PostID);
            return Ok();
        }

        [HttpPut("update-post")]
        public async Task<ActionResult> UpdatePostByIDAsync(PostsUpdateDTO post)
        {
            await postsRepository.PostsUpdate(post);
            return Ok();
        }

        [AllowAnonymous]
        [HttpPost("upload")]
        public async Task<ActionResult> UploadPostFile(IFormFile file)
        { 
            if (file.FileName.EndsWith(".docx") || file.FileName.EndsWith(".pdf"))
            {
                string filePath = Path.Combine(@"C://Uploads/", file.FileName);
                using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
            }
            return Ok();
        }
        #endregion
    }
}
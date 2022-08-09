using Fikra.DAL.Modules.Posts.PostDTO;
using Microsoft.AspNetCore.Http;

namespace Fikra.DAL.Modules.Posts
{
    public interface IPostsRepository
    {
        //READ
        Task<IEnumerable<PostsGetAllDTO>> PostsGetAll(bool forAdmin);
        Task<PostsGetByIDDTO> PostsGetByID(int PostID);
        //CREATE
        Task PostsCreate(PostsCreateDTO post);
        Task PostCreateAnonymous(IFormFile file);
        //UPDATE
        Task PostsUpdate(PostsUpdateDTO post);
        //DELETE
        Task PostsDeleteByID(int PostID);
    }
}

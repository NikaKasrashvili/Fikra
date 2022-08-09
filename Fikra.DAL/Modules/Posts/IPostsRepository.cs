using Fikra.DAL.Modules.Posts.PostDTO;
using FikraModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Posts
{
    public interface IPostsRepository
    {
        //READ
        Task<IEnumerable<PostsGetAllDTO>> PostsGetAll(bool forAdmin);
        Task<PostsGetByIDDTO> PostsGetByID(int PostID);
        //CREATE
        Task PostsCreate(PostsCreateDTO post);
        Task PostCreateAnonymous(string anonymousName, IFormFile file);
        //UPDATE
        Task PostsUpdate(PostsUpdateDTO post);
        //DELETE
        Task PostsDeleteByID(int PostID);
    }
}

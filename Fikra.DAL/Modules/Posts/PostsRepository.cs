using Dapper;
using Fikra.DAL.Modules.Posts.PostDTO;
using FikraModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Posts
{
    public class PostsRepository : IPostsRepository
    {
        #region Constructor
        private readonly string connectionString;

        public PostsRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("default");
        }
        #endregion

        #region Methods
        public async Task<IEnumerable<PostsGetAllDTO>> PostsGetAll(bool forAdmin)
        {
            IEnumerable<PostsGetAllDTO> posts;
            using (var connection = new SqlConnection(connectionString))
            {
                posts = await connection.QueryAsync<PostsGetAllDTO>(
                    nameof(PostsGetAll),
                    new { ForAdmin = forAdmin }, 
                    commandType: CommandType.StoredProcedure
                    );
            }
            return posts;
        }

        public async Task<PostsGetByIDDTO> PostsGetByID(int PostID)
        {
            PostsGetByIDDTO post;
            using (var connection = new SqlConnection(connectionString))
            {
                post = await connection.QuerySingleOrDefaultAsync<PostsGetByIDDTO>(
                    nameof(PostsGetByID),
                    new { PostID = PostID},
                    commandType: CommandType.StoredProcedure
                    );
            }
            return post;
        }

        public async Task PostsCreate(PostsCreateDTO post)
        {
            var par = new DynamicParameters();
            par.Add("PostTitle", post.PostTitle);
            par.Add("PostShortDescription", post.PostShortDescription);
            par.Add("PostFullDescription", post.PostFullDescription);
            par.Add("PostIsPublished", post.PostIsPublished);
            par.Add("PostPublishedByID", post.PostPublishedByID);
            par.Add("PostImageBase64", post.PostImageBase64);
            par.Add("PostSortIndex", post.PostSortIndex);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    nameof(PostsCreate),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task PostsUpdate(PostsUpdateDTO post)
        {
            var par = new DynamicParameters();
            par.Add("PostID", post.PostID);
            par.Add("PostTitle", post.PostTitle);
            par.Add("PostShortDescription", post.PostShortDescription);
            par.Add("PostFullDescription", post.PostFullDescription);
            par.Add("PostAnonymousAuthor", post.PostAnonymousAuthor);
            par.Add("PostIsPublished", post.PostIsPublished);
            par.Add("PostPublishedByID", post.PostPublishedByID);
            par.Add("PostSortIndex", post.PostSortIndex);
            par.Add("PostImageBase64", post.PostImageBase64);

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    nameof(PostsUpdate),
                    par,
                    commandType: CommandType.StoredProcedure);
            }
        }

        public async Task PostsDeleteByID(int PostID)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    nameof(PostsDeleteByID),
                    new { PostID = PostID },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task PostCreateAnonymous(string anonymouseAuthor, IFormFile file)
        {
            var par = new DynamicParameters();

            par.Add("PostAnonymousAuthor", anonymouseAuthor);

            //file saving.. returns string of file location. this string needs to be written to the database

            par.Add("PostFileLocation", "file location string here");

            using (var connection = new SqlConnection(connectionString))
            {
                await connection.ExecuteAsync(
                    nameof(PostCreateAnonymous),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }
        #endregion
    }
}

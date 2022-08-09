using Dapper;
using Fikra.DAL.Modules.Comments.Dto;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Comments
{
    public class CommentsRepository : ICommentsRepository
    {
        #region Constructor
        private readonly string ConnectionString;
        public CommentsRepository(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("default");
        }


        #endregion

        #region Methods
        public async Task CommentCreate(CommentCreateDto commentCreateDto)
        {
            var par = new DynamicParameters();
            par.Add("CommentUserID", commentCreateDto.CommentUserID);
            par.Add("CommentParentID", commentCreateDto.CommentParentID);
            par.Add("CommentText", commentCreateDto.CommentText);
            par.Add("CommentPostID", commentCreateDto.CommentPostID);
           

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(CommentCreate),
                    par,
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task CommentDeleteByID(int commentID)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                    nameof(CommentDeleteByID),
                    new { CommentID = commentID },
                    commandType: CommandType.StoredProcedure
                );
            }
        }

        public async Task<CommentsGetByIDDto> CommentsGetByID(int? CommentID)
        {
            CommentsGetByIDDto comment;
            using (var connection = new SqlConnection(ConnectionString))
            {
                comment = await connection.QuerySingleOrDefaultAsync<CommentsGetByIDDto>(
                    nameof(CommentsGetByID),
                    new { CommentID = CommentID },
                    commandType: CommandType.StoredProcedure
                    );
            }
            return comment;
        }

        public async Task<IEnumerable<CommentsGetByPostIDDto>> CommentsGetByPostID(int postID)
        {
            IEnumerable<CommentsGetByPostIDDto> comments;
            using (var connection = new SqlConnection(ConnectionString))
            {
                comments = await connection.QueryAsync<CommentsGetByPostIDDto>(
                    nameof(CommentsGetByPostID),
                    new { CommentPostID = postID },
                    commandType: CommandType.StoredProcedure
                    );
            }
            return comments;
        }

        public async Task CommentUpdate(CommentUpdateDto commentUpdateDto)
        {
            var par = new DynamicParameters();
            par.Add("CommentID", commentUpdateDto.CommentID);
            par.Add("CommentText", commentUpdateDto.CommentText);

            using (var connection = new SqlConnection(ConnectionString))
            {
                await connection.ExecuteAsync(
                   nameof(CommentUpdate),
                   par,
                   commandType: CommandType.StoredProcedure);
            }
        }
        #endregion
    }
}

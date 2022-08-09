using Fikra.DAL.Modules.Comments.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Comments
{
    public interface ICommentsRepository
    {
        //read
        Task<IEnumerable<CommentsGetByPostIDDto>> CommentsGetByPostID(int postID);
        Task<CommentsGetByIDDto> CommentsGetByID(int? CommentID);

        //create
        Task CommentCreate(CommentCreateDto commentCreateDto);

        //update
        Task CommentUpdate(CommentUpdateDto commentUpdateDto);
        
        //delete
        Task CommentDeleteByID(int commentID);
    }
}

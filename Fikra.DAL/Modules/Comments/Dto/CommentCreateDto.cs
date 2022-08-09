using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Comments.Dto
{
    public class CommentCreateDto
    {
        public int? CommentUserID { get; set; }
        public int? CommentParentID { get; set; }
        public string CommentText { get; set; }
        public int? CommentPostID { get; set; }
    }
}

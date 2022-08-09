using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Comments.Dto
{
    public class CommentsGetByPostIDDto
    {
        public int? CommentID { get; set; }
        public int? CommentUserID { get; set; }
        public string CommentUsername { get; set; }
        public int? CommentParentID { get; set; }
        public string CommentText { get; set; }
        public DateTime? CommentDateCreated { get; set; }
    }
}

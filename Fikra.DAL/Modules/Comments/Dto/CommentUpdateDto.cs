using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Comments.Dto
{
    public class CommentUpdateDto
    {
        public int? CommentID { get; set; }

        public string CommentText { get; set; }
    }
}

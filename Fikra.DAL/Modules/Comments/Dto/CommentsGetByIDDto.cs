namespace Fikra.DAL.Modules.Comments.Dto
{
    public class CommentsGetByIDDto
    {
        public int? CommentID { get; set; }
        public int? CommentUserID { get; set; }
        public string CommentUsername { get; set; }
        public int? CommentParentID { get; set; }
        public string CommentText { get; set; }
        public DateTime? CommentDateCreated { get; set; }
    }
}

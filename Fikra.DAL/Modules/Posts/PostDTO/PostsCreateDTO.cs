using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fikra.DAL.Modules.Posts.PostDTO
{
    public class PostsCreateDTO
    {
        public string PostTitle { get; set; }
        public string PostShortDescription { get; set; }
        public string PostFullDescription { get; set; }
        public bool? PostIsPublished { get; set; }
        public int? PostPublishedByID { get; set; }
        public int? PostSortIndex { get; set; }
        public string PostImageBase64 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialWebsiteMVC5
{
    public class CommentsAndPostID
    {
        public IEnumerable<Comment> comments { get; set; }
        public int PostId { get; set; }
    }
}
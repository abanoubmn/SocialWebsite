using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace SocialWebsiteMVC5
{
    public class FriendRequestsAndPosts
    {
        public IEnumerable<Request> MyRequests { get; set; }
        public ObjectResult<GetAllPosts_Result> MyPosts { get; set; }
        public SocialWebsiteEntities db { get; set; }
        public Guid SessionID { get; set; }

        public FriendRequestsAndPosts(IEnumerable<Request> MyRequests, ObjectResult<GetAllPosts_Result> MyPosts)
        {
            this.MyPosts = MyPosts;
            this.MyRequests = MyRequests;
        }

        public FriendRequestsAndPosts(IEnumerable<Request> MyRequests, ObjectResult<GetAllPosts_Result> MyPosts, SocialWebsiteEntities db)
        {
            this.MyPosts = MyPosts;
            this.MyRequests = MyRequests;
            this.db = db;
        }

        public FriendRequestsAndPosts(IEnumerable<Request> MyRequests, ObjectResult<GetAllPosts_Result> MyPosts, SocialWebsiteEntities db, Guid SessionID)
        {
            this.MyPosts = MyPosts;
            this.MyRequests = MyRequests;
            this.db = db;
            this.SessionID = SessionID;
        }
    }
}
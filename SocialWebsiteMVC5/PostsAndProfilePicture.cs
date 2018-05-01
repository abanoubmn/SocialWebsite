using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;

namespace SocialWebsiteMVC5
{
    public class PostsAndProfilePicture
    {
        public ObjectResult<GetProfileData_Result> Posts { get; set; }
        public ProfilePicture PP { get; set; }
        public Guid SessionID { get; set; }
        public Guid AccountID { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public bool FriendsWith { get; set; }
        public bool Requested { get; set; }
        public bool Requester { get; set; }
        private SocialWebsiteEntities db = new SocialWebsiteEntities();
        
        public PostsAndProfilePicture(Guid AccountID, ObjectResult<GetProfileData_Result> Posts, ProfilePicture PP, Guid SessionID)
        {
            this.AccountID = AccountID;
            this.Posts = Posts;
            this.PP = PP;
            this.SessionID = SessionID;
            this.FullName = db.Accounts.Find(AccountID).FullName;
            this.UserName = db.Accounts.Find(AccountID).UserName;
            if (SessionID.Equals(AccountID))
            {
                FriendsWith = true;
                Requested = false;
            }
            else if (db.Friends.Find(AccountID, SessionID) != null || db.Friends.Find(SessionID, AccountID) != null)
            {
                FriendsWith = true;
                Requested = false;
            }
            else
            {
                FriendsWith = false;
                if (db.Requests.Find(AccountID, SessionID) == null)
                {
                    Requested = false;
                    if (db.Requests.Find(SessionID, AccountID)==null)
                    {
                        Requester = false;
                    }
                    else if (db.Requests.Find(SessionID, AccountID)!=null)
                    {
                        Requester = true;
                    }
                }
                else if (db.Requests.Find(AccountID, SessionID) != null)
                {
                    Requested = true;
                    Requester = false;
                }
            }
        }
    }
}
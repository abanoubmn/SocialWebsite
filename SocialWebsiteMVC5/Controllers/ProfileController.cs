using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SocialWebsiteMVC5.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private SocialWebsiteEntities db = new SocialWebsiteEntities();
        // GET: Profile
        public ActionResult Index(string ID = null)
        {
            var AccountID = new Guid();
            var identity = (ClaimsIdentity)User.Identity;
            if (ID == null)
            {
                AccountID = Guid.Parse(identity.FindFirst("id").Value);
            }
            else
            {
                AccountID = db.Accounts.SingleOrDefault(a => a.UserName == ID).ID;
            }

            var sID = Guid.Parse(identity.FindFirst("id").Value);

            //IEnumerable<Post> posts = db.Posts.Where(a => a.AccountID == AccountID);
            ObjectResult<GetProfileData_Result> posts = db.GetProfileData(AccountID);
            ProfilePicture PP = db.ProfilePictures.Find(AccountID);
            

            PostsAndProfilePicture MyData = new PostsAndProfilePicture(AccountID, posts, PP, sID);
            return View(MyData);
        }

        [HttpPost]
        public ActionResult Add(string RequestedUsername = null)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            var RequestedID = db.Accounts.FirstOrDefault(a => a.UserName == RequestedUsername).ID;
            if (db.Friends.Find(id, RequestedID) == null && db.Friends.Find(RequestedID, id) == null)
            {
                string result = "";
                // to prevent two way requests
                if (db.Requests.Find(id, RequestedID) == null && db.Requests.Find(RequestedID, id) == null)
                {

                    try
                    {
                        SocialWebsiteMVC5.Request NewRequest = new Request()
                        {
                            Requested = RequestedID,
                            Requester = id,
                            RequestID = Guid.NewGuid()
                        };
                        db.Requests.Add(NewRequest);
                        db.SaveChanges();

                        result = ("succeeded");
                    }
                    catch (Exception ex)
                    {
                        result = ("failed");
                        throw ex;
                    }

                }
                return Content(result);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult AcceptFriendRequest(string username)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            int i = 0;
            try
            {
                i = db.AcceptRequest(id, username);
            }
            catch (Exception)
            {

                throw;
            }
            if (i==-1)
            {
                return Content("succeed");
            }
            else
            {
                return Content("failed");
            }
        }

        [HttpPost]
        public ActionResult RejectFriendRequest(string username)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            int i = 0;
            try
            {
                i = db.RejectRequest(id, username);
            }
            catch (Exception)
            {

                throw;
            }
            if (i == -1)
            {
                return Content("succeed");
            }
            else
            {
                return Content("failed");
            }
        }

        [HttpPost]
        public ActionResult UpdatePP(HttpPostedFileBase fileUpload)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            var img = Request.Files[0];
            var imgName = img.FileName;
            var targetFolder = Server.MapPath("~/Images/");
            img.SaveAs(System.IO.Path.Combine(targetFolder, id.ToString()+ System.IO.Path.GetFileName(img.FileName)));
            
            if (db.ProfilePictures.Find(id) == null)
            {
                ProfilePicture NewPP = new ProfilePicture();
                NewPP.AccountID = id;
                NewPP.ImageURL = "/Images/" + id.ToString() + System.IO.Path.GetFileName(img.FileName);
                db.ProfilePictures.Add(NewPP);
            }
            else
            {
                ProfilePicture NewPP = new ProfilePicture() {
                AccountID=id,
                ImageURL= "/Images/" + id.ToString() + System.IO.Path.GetFileName(img.FileName)
                };
                db.ProfilePictures.Remove(db.ProfilePictures.Find(id));
                db.ProfilePictures.Add(NewPP);
            }
            db.SaveChanges();
            return RedirectToAction("");
        }
    }
}
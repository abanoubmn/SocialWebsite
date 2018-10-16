using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SocialWebsiteMVC5.Controllers
{
    public class HomeController : Controller
    {
        private SocialWebsiteEntities db = new SocialWebsiteEntities();

        [ChildActionOnly]
        public ActionResult _ProfilePicture(string id = null)
        {
            var AccID = Guid.Parse(id);
            ProfilePicture PP = db.ProfilePictures.Find(AccID);

            return PartialView(PP);
        }

        public ActionResult Index()
        {
            if (!Request.IsAuthenticated)
            {
                return View();
            }
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse( identity.FindFirst("id").Value);
            
            var FriendRequest = db.Requests.Where(a => a.Requested == id) as IEnumerable<Request>;
            var posts = db.GetAllPosts(id);
            
            var MyData = new FriendRequestsAndPosts(FriendRequest, posts, db, id);
            
            return View("Index1", MyData);
        }

        [Authorize]
        [ChildActionOnly]
        public ActionResult _Requests()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            var FriendRequest = db.Requests.Where(a => a.Requested == id) as IEnumerable<Request>;
            return PartialView(FriendRequest);
        }

        [Authorize]
        public ActionResult Search(string search=null)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var sID = Guid.Parse(identity.FindFirst("id").Value);

            if (search == "")
            {
                return RedirectToAction("/Index");
            }
            else
            {
                var SearchResult = db.SearchAccounts(search, sID);
               
                return View(SearchResult);
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace SocialWebsiteMVC5.Controllers
{
    public class FriendsController : Controller
    {
        private SocialWebsiteEntities db = new SocialWebsiteEntities();

        // GET: Friends
        public ActionResult Index()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            
            return View(db.GetFriendList(id));
        }
    }
}
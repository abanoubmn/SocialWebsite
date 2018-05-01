using System;
using System.Linq;
using System.Security.Claims;
using System.Web.Mvc;

namespace SocialWebsiteMVC5.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private SocialWebsiteEntities db = new SocialWebsiteEntities();

        [HttpPost]
        public ActionResult InsertPost(Post post)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            InsertPost_Result p = new InsertPost_Result();
            try
            {
                if (post.PostContent != null)
                {
                    p= db.InsertPost(id, post.PostContent, DateTime.Now).First();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            
            return PartialView(p);
        }
        [HttpPost]
        public ActionResult Edit(Post post)
        {
            var id = Guid.Parse(((ClaimsIdentity)User.Identity).FindFirst("id").Value);
            int i = 0;
            try
            {
                i=db.EditPost(post.PostID, id, post.PostContent);
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

        [ChildActionOnly]
        public ActionResult _comment(int id)
        {
            return PartialView(new Comment() { PostID = id });
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult _comment(Comment comment)
        {
            var id = Guid.Parse(((ClaimsIdentity)User.Identity).FindFirst("id").Value);
            comment.CommenterID = id;
            db.Comments.Add(comment);
            db.SaveChanges();
            return RedirectToAction("", "Home");
        }

        public ActionResult Comments(int id)
        {
            var comments = db.Comments.Where(c => c.PostID == id);

            return PartialView(new CommentsAndPostID(){comments=comments, PostId=id});
        }

        [HttpPost]
        public ActionResult _Posts(Post post)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var id = Guid.Parse(identity.FindFirst("id").Value);
            try
            {
                if (post.PostContent != null)
                {
                    post.AccountID = id;
                    post.DateCreated = DateTime.Now;
                    db.Posts.Add(post);
                    db.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                
            }
            return PartialView(db.GetAllPosts(id));
        }

        [HttpPost]
        public ActionResult LikePost(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var accountId = Guid.Parse(identity.FindFirst("id").Value);
            int i=0;
            try
            {
                 i=db.InsertLike(accountId, id);
            }
            catch (Exception)
            {
                
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
        public ActionResult UnlikePost(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var accountId = Guid.Parse(identity.FindFirst("id").Value);
            int i = 0;
            try
            {
                i = db.DeleteLike(accountId, id);
            }
            catch (Exception)
            {

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

        //[HttpPost]
        public ActionResult LikeCount(int id)
        {
            int count = (int) db.LikesCount(id).First();
            return Content(count+"");
        }

        public ActionResult CheckLike(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var accountId = Guid.Parse(identity.FindFirst("id").Value);
            int i = (int)db.CheckLike(accountId, id).First() ;
            
            return Content(i + "");
        }

        
        public ActionResult Delete(int id)
        {
            var identity = (ClaimsIdentity)User.Identity;
            var accountId = Guid.Parse(identity.FindFirst("id").Value);
            int i = 0;
            try
            {
                i=db.DeletePost(id, accountId);
            }
            catch(Exception ex)
            {

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
    }
}
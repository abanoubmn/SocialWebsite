using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SocialWebsiteMVC5.Controllers
{
    public class SWApiController : ApiController
    {
        private SocialWebsiteEntities db = new SocialWebsiteEntities();

        [Authorize]
        public ObjectResult<GetLikers_Result> GetLikers(int id)
        {
            ObjectResult<GetLikers_Result> likers = db.GetLikers(id);
            return likers;
        }
    }
}

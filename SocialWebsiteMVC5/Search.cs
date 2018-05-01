using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SocialWebsiteMVC5
{
    public class Search
    {
        //public IEnumerable<SearchAccounts_Result> SearchResult { get; set; }
        //public Guid AccountID { get; set; }
        //public Search(IEnumerable<SearchAccounts_Result> SearchResult, Guid AccountID)
        //{
        //    this.SearchResult = SearchResult;
        //    this.AccountID = AccountID;
        //    SocialWebsiteEntities db = new SocialWebsiteEntities();
        //    foreach (var item in SearchResult)
        //    {
        //        if (db.Friends.Find(AccountID, item.ID) == null && db.Friends.Find(item.ID, AccountID) == null)
        //        {
        //            item.FriendsWith = false;
        //            if (db.Requests.Find(item.ID, AccountID) != null)
        //            {
        //                item.Requested = true;
        //            }
        //            else
        //            {
        //                item.Requested = false;
        //            }
        //        }
        //        else
        //        {
        //            item.Requested = false;
        //            item.FriendsWith = true;
        //        }
        //    }
        //}
    }
}
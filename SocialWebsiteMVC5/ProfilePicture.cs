//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SocialWebsiteMVC5
{
    using System;
    using System.Collections.Generic;
    
    public partial class ProfilePicture
    {
        public string ImageURL { get; set; }
        public System.Guid AccountID { get; set; }
    
        public virtual Account Account { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Loiloc.Models
{
    public partial class tblGuarantee
    {
        public int id { get; set; }
        public Nullable<int> idManu { get; set; }
        public Nullable<int> idDistrict { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string TimeWork { get; set; }
        public string Tag { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<int> Ord { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<int> idUser { get; set; }
    }
}

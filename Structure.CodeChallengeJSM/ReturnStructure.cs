using System;
using System.Collections.Generic;

namespace Structure.CodeChallengeJSM
{
    public class User : StructureCommon
    {
        public string type { get; set; }
        public string gender { get; set; }
        public Name name { get; set; }
        public Location location { get; set; }
        public string email { get; set; }
        public DateTime birthday { get; set; }
        public DateTime registered { get; set; }
        public List<string> telephoneNumbers { get; set; }
        public List<string> mobileNumbers { get; set; }
        public Picture picture { get; set; }
        public string nationality { get; set; }
    }

    public class ReturnStructure
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalCount { get; set; }
        public List<User> users { get; set; }
    }


}

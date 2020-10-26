using System;
using System.Collections.Generic;
using System.Text;

namespace Structure.CodeChallengeJSM
{
        public class Dob
        {
            public DateTime date { get; set; }
        }

        public class Registered
        {
            public DateTime date { get; set; }
        }
        public class Result : StructureCommon
        {
            public string gender { get; set; }
            public Name name { get; set; }
            public Location location { get; set; }
            public string email { get; set; }
            public Dob dob { get; set; }
            public Registered registered { get; set; }
            public string phone { get; set; }
            public string cell { get; set; }
            public Picture picture { get; set; }
            public string tipoCliente { get; set; }
        }

        public class StructureReturnJson
    {
            public List<Result> results { get; set; }
        }
}

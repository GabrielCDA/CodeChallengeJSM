using System;
using System.Collections.Generic;
using System.Text;

namespace Structure.CodeChallengeJSM
{
    public class StructureReturnCSV
    {
        public string gender { get; set; }
        public string name__title { get; set; }
        public string name__first { get; set; }
        public string name__last { get; set; }
        public string location__street { get; set; }
        public string location__city { get; set; }
        public string location__state { get; set; }
        public string location__postcode { get; set; }
        public string location__coordinates__latitude { get; set; }
        public string location__coordinates__longitude { get; set; }
        public string location__timezone__offset { get; set; }
        public string location__timezone__description { get; set; }
        public string email { get; set; }
        public DateTime dob__date { get; set; }
        public DateTime registered__date { get; set; }
        public string phone { get; set; }
        public string cell { get; set; }
        public string picture__large { get; set; }
        public string picture__medium { get; set; }
        public string picture__thumbnail { get; set; }
    }
}

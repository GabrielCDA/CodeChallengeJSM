using System;
using System.Collections.Generic;
using System.Text;

namespace Structure.CodeChallengeJSM
{
    public class StructureCommon
    {
        public class Name
        {
            public string title { get; set; }
            public string first { get; set; }
            public string last { get; set; }
        }
        public class Coordinates
        {
            public string latitude { get; set; }
            public string longitude { get; set; }
        }

        public class Timezone
        {
            public string offset { get; set; }
            public string description { get; set; }
        }

        public class Location
        {
            public string region { get; set; }
            public string street { get; set; }
            public string city { get; set; }
            public string state { get; set; }
            public int postcode { get; set; }
            public Coordinates coordinates { get; set; }
            public Timezone timezone { get; set; }
        }

        public class Picture
        {
            public string large { get; set; }
            public string medium { get; set; }
            public string thumbnail { get; set; }
        }
    }
}

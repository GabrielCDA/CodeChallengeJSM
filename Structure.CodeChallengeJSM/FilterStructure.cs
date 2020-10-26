
using System.Collections.Generic;

namespace Structure.CodeChallengeJSM
{
    public class Filter
    {
        public int pageSize { get; set; }
        public int pageNumber { get; set; }
        public List<User> users { get; set; }

    }
    public class FilterStructure
    {
        public Filter filter { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataMapping
{
    class ComparisonItem
    {
        public int FirstID { get; set; }
        public string FirstDescription { get; set; }
        public int PercentageMatching { get; set; }
        public int SecondID { get; set; }
        public string SecondDescription { get; set; }
        public string Status { get; set; }
    }
}

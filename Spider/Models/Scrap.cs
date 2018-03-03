using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spider.Models
{
    public class Scrap
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public int Rank { get; set; }
        public string Domain { get; set; }
        public string Url { get; set; }
        public string TagsValue { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Cones.Models
{
    public class Quote
    {
        public string quoteText { get; set; }
        public string quoteAuthor { get; set; }
        public string senderName { get; set; }
        public string senderLink { get; set; }
        public string quoteLink { get; set; }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cones.Models
{
    public class Users
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "Gender")]
        public string Gender { get; set; }
        [JsonProperty(PropertyName = "userId")]
        public string userId { get; set; }
        [JsonProperty(PropertyName = "MinAge")]
        public int MinAge { get; set; }
        [JsonProperty(PropertyName = "DateEntry")]
        public DateTime dateentry { get; set; }

        public Users(string name, string gender, string id, int minage, DateTime date)
        {
            this.Name = name;
            this.Gender = gender;
            this.userId = id;
            this.MinAge = minage;
            this.dateentry = date;
        }
    }
}

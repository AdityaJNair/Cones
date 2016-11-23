using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cones.Models
{
    public class Timeline
    {
        [JsonProperty(PropertyName = "Id")]
        public string ID { get; set; }

        public string Emotion { get; set; }
        public DateTime Date { get; set; }
        public string RecommendedFlavour { get; set; }
        public string FlavourFilename { get; set; }
        public string Userid { get; set; }
        public string EmotionDate { get; set; }
        public string RecommendedText { get; set; }

        public Timeline(string emotion, string recommendedflavour, string filename, string userid)
        {
            this.Emotion = emotion;
            this.Date = DateTime.Now;
            this.RecommendedFlavour = recommendedflavour;
            this.FlavourFilename = filename;
            this.Userid = userid;
            this.EmotionDate = this.Emotion + " on " + string.Format("{0:dd/MM/yy}", Date);
            this.RecommendedText = "Recommended flavour : " + RecommendedFlavour;
        }
    }
}

using Newtonsoft.Json;

namespace FacebookLogin.Models
{
    public class FacebookProfile
    {
        public string Name { get; set; }
        public Picture Picture { get; set; }
        [JsonProperty("age_range")]
        public AgeRange AgeRange { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; }
        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Id { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class AgeRange
    {
        public int Min { get; set; }
    }

    public class Data
    {
        public bool IsSilhouette { get; set; }
        public string Url { get; set; }
    }

}

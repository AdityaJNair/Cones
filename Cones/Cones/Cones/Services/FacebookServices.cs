using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FacebookLogin.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FacebookLogin.Services
{
    /// <summary>
    /// Service that asks for async http request with graph api and deserialises the json response object into a facebookprofile object
    /// </summary>
    public class FacebookServices
    {

        public async Task<FacebookProfile> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=name,picture.width(550).height(550),age_range,first_name,last_name,gender&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfile>(userJson);

            return facebookProfile;
        }
    }
}

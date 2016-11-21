using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mobileapp.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace mobileapp.Services
{
    class FacebookServices
    {
        /// <summary>
        /// Getting the data for a facebook profile
        /// https://github.com/HoussemDellai/Facebook-Login-Xamarin-Forms/blob/master/FacebookLogin/FacebookLogin/Services/FacebookServices.cs
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public async Task<ProfileFB> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=name,picture,work,website,religion,location,locale,link,cover,age_range,bio,birthday,devices,email,first_name,last_name,gender,hometown,is_verified,languages&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            var facebookProfile = JsonConvert.DeserializeObject<ProfileFB>(userJson);

            return facebookProfile;
        }
    }
}

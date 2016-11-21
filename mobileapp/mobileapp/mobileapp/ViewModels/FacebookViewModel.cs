using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using mobileapp.Models;
using mobileapp.Services;
using mobileapp.ViewModels;

namespace mobileapp.ViewModels
{
    class FacebookViewModel : INotifyPropertyChanged
    {
        private ProfileFB _facebookProfile;


        /// <summary>
        /// Gets the users facebook profile
        /// </summary>
        public ProfileFB FacebookProfile
        {
            get
            {
                return _facebookProfile;
            }

            set
            {
                _facebookProfile = value;
                OnPropertyChanged();
            }
        }

        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();

            FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
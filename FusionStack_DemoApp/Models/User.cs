using Newtonsoft.Json;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionStack_DemoApp.Models
{
    public class User : BindableBase
    {
        private string _userId;
        [JsonIgnore]
        public string UserId
        {
            get { return _userId; }
            set { SetProperty(ref _userId, value); }
        }
        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }
    }
}

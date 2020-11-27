using Newtonsoft.Json;
using Prism.Mvvm;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace FusionStack_DemoApp.Models
{
    public class StudentInfo : BindableBase
    {
        private int _id;
        [PrimaryKey]
        public int id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }
        private string _firstname;
        public string firstName
        {
            get { return _firstname; }
            set { SetProperty(ref _firstname, value); }
        }
        private string _lastName;
        public string lastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _emailID;
        public string email
        {
            get { return _emailID; }
            set { SetProperty(ref _emailID, value); }
        }
        private string _phoneNo;
        public string phoneNumber
        {
            get { return _phoneNo; }
            set { SetProperty(ref _phoneNo, value); }
        }
        private string _address;
        public string address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }
        private int _age;
        public int age
        {
            get { return _age; }
            set { SetProperty(ref _age, value); }
        }
        private bool _isSync;
        [JsonIgnore]
        public bool IsSynced
        {
            get { return _isSync; }
            set { SetProperty(ref _isSync, value); }
        }
    }
    public class RootStudents
    {
        public List<StudentInfo> students { get; set; }
    }
}

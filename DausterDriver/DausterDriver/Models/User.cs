using System;
using System.Collections.Generic;
using System.Text;

namespace DausterDriver.Models
{
    public class User
    {
        public string name { get; set; }
        public string surnames { get; set; }
        public DateTime birthday { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string avatar { get; set; }
        public string password { get; set; }
        public string password_confirmation { get; set; }
        public int vehicles_id { get; set; }
        public int current_work_id { get; set; }
        public int data_plan { get; set; }
    }
}

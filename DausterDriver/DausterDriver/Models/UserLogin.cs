using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace DausterDriver.Models
{
    public class UserLogin
    {
        public bool success { get; set; }

        public JObject error { get; set; }

        public string token { get; set; }

        public int nextStep { get; set; }
    }
}

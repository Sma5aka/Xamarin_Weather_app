using System;
using System.Collections.Generic;
using System.Text;

namespace Weather_HWG
{
    public class Coord_Response
    {
        public string name { get; set; }
        public Local_names local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
    }

    public class Local_names
    {
        public string en { get; set; }
        public string ru { get; set; }
    }
    
    public class Weather_response
    {

    }

}

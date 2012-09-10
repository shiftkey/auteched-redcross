using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lasseter.Entities
{
    public class PostCode
    {
        public int ID { get; set; }
        public String Locality { get; set; }
        public String State { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PostCode1 { get; set; }
        
    }

}

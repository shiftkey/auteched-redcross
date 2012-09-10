using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lasseter.Entities
{
    public class Person
    {
        [DataMember (Name="ID")]
        public int UniqueId { get; set; }
        public String Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }



}

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
        public int ID { get; set; }
        [DataMember (Name="ID")]
        public int UniqueId { get { return ID; } set { UniqueId = value; } }
        public String Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int OnDuty { get; set; }
    }



}

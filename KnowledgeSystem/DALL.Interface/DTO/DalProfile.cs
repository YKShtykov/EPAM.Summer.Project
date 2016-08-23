using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public class DalProfile:IEntity
    {
        public int Id { get; set; }

        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string RelationshipStatus { get; set; }
        public string City { get; set; }
        public string AdditionalInfo { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
    }
}

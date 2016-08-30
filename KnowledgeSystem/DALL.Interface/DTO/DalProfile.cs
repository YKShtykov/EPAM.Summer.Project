using System;

namespace DAL.Interface
{
    /// <summary>
    /// DAL Layout Profile class. It stores information about user owner of the profile
    /// </summary>
    public class DalProfile:IEntity
    {
        /// <summary>
        /// DAL Profile identity number
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User last name 
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// User first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// User middle name
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// User contact Email
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// User contact phone
        /// </summary>
        public int ContactPhone { get; set; }

        /// <summary>
        /// User dirth date
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// User gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// User relationship status
        /// </summary>
        public string RelationshipStatus { get; set; }

        /// <summary>
        /// User City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// User additional info
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// User photo
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// User photo MIME type
        /// </summary>
        public string ImageMimeType { get; set; }
    }
}

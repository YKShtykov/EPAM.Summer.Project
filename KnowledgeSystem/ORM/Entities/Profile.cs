namespace ORM
{
    /// <summary>
    /// ORM Layout Profile Class. It stores information about user owner of the profile
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Profile Identify number
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
        /// User birth date
        /// </summary>
        public string BirthDate { get; set; }

        /// <summary>
        /// User contact Email
        /// </summary>
        public string ContactEmail { get; set; }

        /// <summary>
        /// User contact phone
        /// </summary>
        public int ContactPhone { get; set; }

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
        /// Additional info about user
        /// </summary>
        public string AdditionalInfo { get; set; }

        /// <summary>
        /// User photo
        /// </summary>
        public byte[] Image { get; set; }

        /// <summary>
        /// photo MIME type
        /// </summary>
        public string ImageMimeType { get; set; }


        /// <summary>
        /// ORM user, owner of the profile. It need for DataBase creating
        /// </summary>
        public User User { get; set; }
    }
}

namespace FinalAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public byte[] ProfilePicture { get; set; }


        public User(String username, String email, String password)
        {
            this.Username = username;
            this.Email = email;
            this.Password = password;

            string defaultPictureFilePath = "./Images/UserDefaultImage.jpg";
            this.ProfilePicture = File.ReadAllBytes(defaultPictureFilePath);
        }
    }

}

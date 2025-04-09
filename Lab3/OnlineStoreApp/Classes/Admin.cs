namespace OnlineStoreApp.Classes
{
    public class Admin : IUser
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string AdminPassword { get; set; }

        public Admin() {}
        public Admin(int id, string name, string lastName, string email, string password)
        {
            ID = id;
            FirstName = name;
            LastName = lastName;
            Email = email;
            AdminPassword = password;
        }

        public int getID()
        {
            return ID;
        }

        public string getFirstName()
        {
            return FirstName;
        }

        public string getLastName()
        {
            return LastName;
        }

        public string getEmail()
        {
            return Email;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public void setFirstName(string firstName)
        {
            FirstName = firstName;
        }

        public void setLastName(string lastName)
        {
            LastName = lastName;
        }

        public void setEmail(string email)
        {
            Email = email;
        }
        public bool Authenticate(string password)
        {
            return AdminPassword == password;
        } 
        
    }
}
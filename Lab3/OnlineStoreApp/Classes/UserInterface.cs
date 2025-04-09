namespace OnlineStoreApp.Classes
{
    public interface IUser
    {
        int ID { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        int getID();
        string getFirstName();
        string getLastName();
        string getEmail();
        void setID(int id); 
        void setFirstName(string firstName);
        void setLastName(string lastName);
        void setEmail(string email);
    }
}
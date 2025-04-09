namespace OnlineStoreApp.Classes
{
    public class User: IUser
    {
        public int ID { get ; set ; }
        public string FirstName { get ; set ; }
        public string LastName { get ; set ; }
        public string Password { get ; set ; }
        public string Email { get ; set ; }
        public List<Order> Orders { get; set; } = new List<Order>();
        public User() { }
        public User(int id, string name, string lastName,  string email, string password)
        {
            ID = id;
            FirstName = name;
            LastName = lastName;
            Email = email;
            Password = password;
        }
    

        public override string ToString()
        {
            return $"Id: {ID}, User: {FirstName}, Email: {Email}";
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
        public void addOrder(Order order)
        {
            Orders.Add(order);
        }
        public void removeOrder(Order order)
        {
            Orders.Remove(order);
        }
        public List<Order> getOrders()
        {
            return Orders;
        }
        public void setOrders(List<Order> orders)
        {
            Orders = orders;
        }
        public string getPassword()
        {
            return Password;
        }
        public void setPassword(string password)
        {
            Password = password;
        }
        public bool Authenticate(string password)
        {
            return Password == password;
        }   
    }
}
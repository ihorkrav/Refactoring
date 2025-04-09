namespace OnlineStoreApp.Classes
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public Product() { }
        public Product(int id, string name, string description, decimal price, int stock)
        {
            ID = id;
            Name = name;
            Description = description;
            Price = price;
            Stock = stock;
        }

        public override string ToString()
        {
            return $"Id: {ID}, Product: {Name}, Price: {Price}, Stock: {Stock}";
        }

        public int getID()
        {
            return ID;
        }

        public string getName()
        {
            return Name;
        }

        public string getDescription()
        {
            return Description;
        }

        public decimal getPrice()
        {
            return Price;
        }

        public int getStock()
        {
            return Stock;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public void setName(string name)
        {
            Name = name;
        }

        public void setDescription(string description)
        {
            Description = description;
        }

        public void setPrice(decimal price)
        {
            Price = price;
        }

        public void setStock(int stock)
        {
            Stock = stock;
        }
    }
}
namespace OnlineStoreApp.Classes
{
    public class Order
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public List<Product> Products { get; set; }
        public decimal Price { get; set; }
        public bool Oredered { get; set; }
        public bool Delivered { get; set; }
        public Order() { }
        public Order(int id, int userId, List<Product> products, bool Oredered, bool Delivered)
        {
            ID = id;
            UserID = userId;
            Products = products;
            this.Oredered = Oredered;
            this.Delivered = Delivered;
        }
        
        public override string ToString()
        {
            return $"Order ID: {ID}, User ID: {UserID}, Total Price: {Price}, Delivered: {Delivered}, Ordered: {Oredered}";
        }

        public int getID()
        {
            return ID;
        }

        public int getUserID()
        {
            return UserID;
        }

        public List<Product> getProducts()
        {
            return Products;
        }

        public decimal getPrice()
        {
            return Price;
        }

        public bool getOredered()
        {
            return Oredered;
        }

        public bool getDelivered()
        {
            return Delivered;
        }

        public void setID(int id)
        {
            ID = id;
        }

        public void setUserID(int userId)
        {
            UserID = userId;
        }

        public void setProducts(List<Product> products)
        {
            Products = products;
        }

        public void setPrice(decimal Price)
        {
            this.Price = Price;
        }

        public void setOredered(bool oredered)
        {
            Oredered = oredered;
        }

        public void setDelivered(bool delivered)
        {
            Delivered = delivered;
        }

        public void calculatePrice()
        {
            Price = 0;
            foreach (var product in Products)
            {
                Price += product.Price;
            }
        }
    }

}
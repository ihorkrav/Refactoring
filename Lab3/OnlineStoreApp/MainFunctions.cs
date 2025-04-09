using System;
using System.Collections.Generic;
using System.Linq;
using OnlineStoreApp.Classes;

namespace OnlineStoreApp
{
    public class StoreManager
    {
        public List<User> users = new List<User>();
        public List<Admin> admins = new List<Admin>();
        public List<Product> products = new List<Product>();
        public List<Order> orders = new List<Order>();

        private int userIdCounter = 1;
        private int adminIdCounter = 1;
        private int productIdCounter = 1;
        private int orderIdCounter = 1;

        public void RegisterUser()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last name: ");
            var lastName = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var user = new User(userIdCounter++, firstName, lastName, email, password);
            user.setPassword(password);
            users.Add(user);

            Console.WriteLine("User registered successfully.");
        }

        public void RegisterAdmin()
        {
            Console.Write("First name: ");
            var firstName = Console.ReadLine();
            Console.Write("Last name: ");
            var lastName = Console.ReadLine();
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var admin = new Admin(adminIdCounter++, firstName, lastName, email, password);
            admins.Add(admin);

            Console.WriteLine("Admin registered successfully.");
        }

        public void LoginUser()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var user = users.FirstOrDefault(u => u.getEmail() == email && u.Authenticate(password));

            if (user != null)
            {
                Console.WriteLine($"Welcome, {user.getFirstName()}!");
                UserMenu(user);
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
        }

        public void LoginAdmin()
        {
            Console.Write("Email: ");
            var email = Console.ReadLine();
            Console.Write("Password: ");
            var password = Console.ReadLine();

            var admin = admins.FirstOrDefault(a => a.getEmail() == email && a.Authenticate(password));

            if (admin != null)
            {
                Console.WriteLine($"Welcome, Admin {admin.getFirstName()}!");
                AdminMenu();
            }
            else
            {
                Console.WriteLine("Invalid credentials.");
            }
        }

        private void UserMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("\nUser Menu:");
                Console.WriteLine("1. View Products");
                Console.WriteLine("2. Place Order");
                Console.WriteLine("3. View My Orders");
                Console.WriteLine("0. Logout");

                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": DisplayProducts(); break;
                    case "2": PlaceOrder(user); break;
                    case "3": ViewUserOrders(user); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        private void AdminMenu()
        {
            while (true)
            {
                Console.WriteLine("\nAdmin Menu:");
                Console.WriteLine("1. Add Product");
                Console.WriteLine("2. Delete Product");
                Console.WriteLine("3. View All Orders");
                Console.WriteLine("4. Delete User");
                Console.WriteLine("0. Logout");

                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1": AddProduct(); break;
                    case "2": DeleteProduct(); break;
                    case "3": ViewAllOrders(); break;
                    case "4": DeleteUser(); break;
                    case "0": return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        private void DeleteUser()
        {
            Console.WriteLine("Enter an ID to delete a user: ");
            int id = int.Parse(Console.ReadLine());
            var user = users.FirstOrDefault(u => u.getID() == id);
            if (user != null)
            {
                users.Remove(user);
                Console.WriteLine("User removed.");
            }
            else
            {
                Console.WriteLine("User not found.");
            }
        }

        private void AddProduct()
        {
            Console.Write("Product name: ");
            var name = Console.ReadLine();
            Console.Write("Description: ");
            var desc = Console.ReadLine();
            Console.Write("Price: ");
            var price = decimal.Parse(Console.ReadLine());
            Console.Write("Stock: ");
            var stock = int.Parse(Console.ReadLine());

            var product = new Product(productIdCounter++, name, desc, price, stock);
            products.Add(product);

            Console.WriteLine("Product added.");
        }

        private void DeleteProduct()
        {
            DisplayProducts();
            Console.Write("Enter product ID to delete: ");
            int id = int.Parse(Console.ReadLine());

            var product = products.FirstOrDefault(p => p.getID() == id);
            if (product != null)
            {
                products.Remove(product);
                Console.WriteLine("Product removed.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private void DisplayProducts()
        {
            Console.WriteLine("\nAvailable Products:");
            foreach (var p in products)
            {
                Console.WriteLine(p);
            }
        }

        private void PlaceOrder(User user)
        {
            var cart = new List<Product>();
            DisplayProducts();

            while (true)
            {
                Console.Write("Enter product ID to add to cart (or 0 to finish): ");
                int id = int.Parse(Console.ReadLine());
                if (id == 0) break;

                var product = products.FirstOrDefault(p => p.getID() == id);
                if (product != null && product.getStock() > 0)
                {
                    cart.Add(product);
                    product.setStock(product.getStock() - 1);
                    Console.WriteLine($"{product.getName()} added to cart.");
                }
                else
                {
                    Console.WriteLine("Product not available.");
                }
            }

            if (cart.Count > 0)
            {
                var order = new Order(orderIdCounter++, user.getID(), cart, true, false);
                order.calculatePrice();
                orders.Add(order);
                user.addOrder(order);
                Console.WriteLine("Order placed!");
            }
            else
            {
                Console.WriteLine("Cart is empty. Order not placed.");
            }
        }

        private void ViewUserOrders(User user)
        {
            var userOrders = orders.Where(o => o.getUserID() == user.getID()).ToList();

            if (userOrders.Count == 0)
            {
                Console.WriteLine("You have no orders.");
            }
            else
            {
                Console.WriteLine("\nYour Orders:");
                foreach (var order in userOrders)
                {
                    Console.WriteLine(order);
                }
            }
        }

        private void ViewAllOrders()
        {
            if (orders.Count == 0)
            {
                Console.WriteLine("No orders in the system.");
            }
            else
            {
                Console.WriteLine("\nAll Orders:");
                foreach (var order in orders)
                {
                    Console.WriteLine(order);
                }
            }
        }
    }
}

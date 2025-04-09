using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualBasic;
using OnlineStoreApp;
using OnlineStoreApp.Classes;

class Program
{
    StoreManager storeManager = new StoreManager();
    static List<User> users = new List<User>();
    static List<Admin> admins = new List<Admin>();
    static List<Product> products = new List<Product>();
    static List<Order> orders = new List<Order>();
    static int userIdCounter = 1;
    static int adminIdCounter = 1;
    static int productIdCounter = 1;
    static int orderIdCounter = 1;

    static void Main()
    {
        StoreManager storeManager = new StoreManager();

        Console.WriteLine("=== Welcome to the Online Store System ===");

        while (true)
        {
            Console.WriteLine("\nSelect an option:");
            Console.WriteLine("1. Register as User");
            Console.WriteLine("2. Register as Admin");
            Console.WriteLine("3. Login as User");
            Console.WriteLine("4. Login as Admin");
            Console.WriteLine("0. Exit");
            Console.Write("Choice: ");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    storeManager.RegisterUser();
                    break;
                case "2":
                    storeManager.RegisterAdmin();
                    break;
                case "3":
                    storeManager.LoginUser();
                    break;
                case "4":
                    storeManager.LoginAdmin();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }

   
}

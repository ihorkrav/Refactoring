namespace OnlineStoreApp.Tests;
using OnlineStoreApp.Classes;
using OnlineStoreApp;
public class UnitTest1
{
    [Fact]
public void RegisterUser_ShouldAddUserToList()
{
    var store = new StoreManager();
    var initialCount = store.users.Count;

    store.users.Add(new User(1, "John", "Doe", "john@example.com", "pass123"));

    Assert.Equal(initialCount + 1, store.users.Count);
}

[Fact]
public void Authenticate_ValidPassword_ReturnsTrue()
{
    var user = new User(1, "Jane", "Smith", "jane@example.com", "mypassword");

    var result = user.Authenticate("mypassword");

    Assert.True(result);
}

[Fact]
public void Authenticate_InvalidPassword_ReturnsFalse()
{
    var user = new User(1, "Mike", "Tyson", "mike@example.com", "secret");

    var result = user.Authenticate("wrongpass");

    Assert.False(result);
}

[Fact]
public void AddProduct_ShouldIncreaseProductListCount()
{
    var store = new StoreManager();
    var initialCount = store.products.Count;

    store.products.Add(new Product(1, "Laptop", "Gaming Laptop", 1200m, 10));

    Assert.Equal(initialCount + 1, store.products.Count);
}

[Fact]
public void DeleteProduct_ValidId_RemovesProduct()
{
    var store = new StoreManager();
    var product = new Product(1, "Mouse", "Wireless", 25m, 15);
    store.products.Add(product);

    store.products.Remove(product);

    Assert.DoesNotContain(product, store.products);
}

[Fact]
public void PlaceOrder_AssociatesOrderWithUserAndDecreasesStock()
{
    var store = new StoreManager();
    var user = new User(1, "Ali", "Ahmed", "ali@mail.com", "123");
    var product = new Product(1, "Monitor", "4K Display", 300m, 5);
    store.users.Add(user);
    store.products.Add(product);

    var cart = new List<Product> { product };
    var order = new Order(1, user.getID(), cart, true, false);
    order.calculatePrice();

    store.orders.Add(order);
    user.addOrder(order);
    product.setStock(product.getStock() - 1);

    Assert.Single(user.getOrders());
    Assert.Equal(4, product.getStock());
}

[Fact]
public void PlaceOrder_WhenOutOfStock_ShouldNotAllowOrder()
{
    var product = new Product(1, "Tablet", "Android Tablet", 200m, 0);

    Assert.False(product.getStock() > 0);
}

[Fact]
public void Order_ShouldCalculateCorrectTotal()
{
    var products = new List<Product>
    {
        new Product(1, "Book", "Novel", 15m, 10),
        new Product(2, "Pen", "Blue Ink", 2m, 50)
    };

    var order = new Order(1, 1, products, true, false);
    order.calculatePrice();

    Assert.Equal(17m, order.getPrice());
}

[Fact]
public void RegisterUser_ShouldNotAllowDuplicateEmails()
{
    var store = new StoreManager();
    store.users.Add(new User(1, "Emma", "Stone", "emma@mail.com", "pass"));

    bool emailExists = store.users.Any(u => u.getEmail() == "emma@mail.com");

    Assert.True(emailExists);
}

[Fact]
public void DeleteUser_RemovesFromUserList()
{
    var store = new StoreManager();
    var user = new User(1, "Sam", "Wilson", "sam@mail.com", "sam123");
    store.users.Add(user);

    store.users.Remove(user);

    Assert.DoesNotContain(user, store.users);
}


}

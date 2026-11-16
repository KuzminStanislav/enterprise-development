using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Tests;

/// <summary>
/// Тестовый набор данных для проверки запросов службы доставки еды
/// </summary>
public class QueriesTestFixture
{
    /// <summary>
    /// Список клиентов
    /// </summary>
    public readonly List<Customer> Customers;

    /// <summary>
    /// Список ресторанов
    /// </summary>
    public readonly List<Restaurant> Restaurants;

    /// <summary>
    /// Список блюд
    /// </summary>
    public readonly List<Dish> Dishes;

    /// <summary>
    /// Список курьеров
    /// </summary>
    public readonly List<Courier> Couriers;

    /// <summary>
    /// Список заказов
    /// </summary>
    public readonly List<Order> Orders;

    /// <summary>
    /// Инициализация тестовых данных
    /// </summary>
    public QueriesTestFixture()
    {
        Customers = GetCustomers();
        Restaurants = GetRestaurants();
        Dishes = GetDishes();
        Couriers = GetCouriers();
        Orders = GetOrders(Customers, Restaurants, Couriers);
    }

    /// <summary>
    /// Создание списка клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    private static List<Customer> GetCustomers() =>
    [
        new Customer { Id = 1, Name = "Вадим Владимиров", Phone = "+79990000000" },
        new Customer { Id = 2, Name = "Иван Лапшин", Phone = "+79991112233" },
        new Customer { Id = 3, Name = "Максим Громов", Phone = "+79992223344" },
        new Customer { Id = 4, Name = "Анна Иванова", Phone = "+79993334455" },
        new Customer { Id = 5, Name = "Иван Петров", Phone = "+79994445566" },
        new Customer { Id = 6, Name = "Елена Сидорова", Phone = "+79995556677" },
        new Customer { Id = 7, Name = "Дмитрий Волков", Phone = "+79996667788" },
        new Customer { Id = 8, Name = "Ольга Морозова", Phone = "+79997778899" },
        new Customer { Id = 9, Name = "Сергей Васильев", Phone = "+79998889900" },
        new Customer { Id = 10, Name = "Мария Смирнова", Phone = "+79999990011" }
    ];

    /// <summary>
    /// Создаёт список ресторанов
    /// </summary>
    /// <returns>Список ресторанов</returns>
    private static List<Restaurant> GetRestaurants() =>
    [
        new Restaurant { Id = 1, Name = "Вкусно — и точка", Address = "ул. Полевая, д. 15", IsActive = true },
        new Restaurant { Id = 2, Name = "Крошка картошка", Address = "ул. Молодогвардейская, д. 135", IsActive = true },
        new Restaurant { Id = 3, Name = "Dodo Pizza", Address = "ул. Ново-Садовая, д. 8А", IsActive = true },
        new Restaurant { Id = 4, Name = "Бургер Кинг", Address = "ул. Дыбенко, д. 30", IsActive = true },
        new Restaurant { Id = 5, Name = "Rostic's", Address = "ул. Авроры, д. 209", IsActive = false },
        new Restaurant { Id = 6, Name = "Перчини", Address = "Московское ш., д. 252", IsActive = true },
        new Restaurant { Id = 7, Name = "Суши Wok", Address = "ул. 6-я просека, д. 149", IsActive = true },
        new Restaurant { Id = 8, Name = "Папа Джонс", Address = "ул. Георгия Димитрова, д. 110", IsActive = true },
        new Restaurant { Id = 9, Name = "Старик Хинкалыч", Address = "Московское ш., д. 205", IsActive = true },
        new Restaurant { Id = 10, Name = "Тануки", Address = "ул. Дачная, д. 2", IsActive = true }
    ];

    /// <summary>
    /// Создаёт список блюд
    /// </summary>
    /// <param name="restaurants">Список ресторанов для привязки</param>
    /// <returns>Список блюд</returns>
    private static List<Dish> GetDishes() =>
    [
        new Dish { Id = 1, RestaurantId = 1, Name = "Гранд", Price = 190, Category = DishCategory.FastFood },
        new Dish { Id = 2, RestaurantId = 1, Name = "Картофель фри", Price = 100, Category = DishCategory.FastFood },
        new Dish { Id = 3, RestaurantId = 2, Name = "Печень жареная с луком", Price = 350, Category = DishCategory.MainCourse },
        new Dish { Id = 4, RestaurantId = 3, Name = "Маргарита", Price = 490, Category = DishCategory.FastFood },
        new Dish { Id = 5, RestaurantId = 4, Name = "Воппер", Price = 250, Category = DishCategory.FastFood },
        new Dish { Id = 6, RestaurantId = 6, Name = "Блин с грибами", Price = 220, Category = DishCategory.MainCourse },
        new Dish { Id = 7, RestaurantId = 7, Name = "Филадельфия", Price = 550, Category = DishCategory.MainCourse },
        new Dish { Id = 8, RestaurantId = 8, Name = "Пепперони", Price = 600, Category = DishCategory.FastFood },
        new Dish { Id = 9, RestaurantId = 9, Name = "Капучино", Price = 180, Category = DishCategory.Drink },
        new Dish { Id = 10, RestaurantId = 10, Name = "Удон с курицей", Price = 420, Category = DishCategory.MainCourse }
    ];

    /// <summary>
    /// Создаёт список курьеров
    /// </summary>
    /// <returns>Список курьеров</returns>
    private static List<Courier> GetCouriers() =>
    [
        new Courier { Id = 1, FullName = "Максим Краснов", Phone = "+79991000001", Vehicle = "Lada Granta", IsActive = true },
        new Courier { Id = 2, FullName = "Алексей Смирнов", Phone = "+79991000002", Vehicle = "Велосипед", IsActive = true },
        new Courier { Id = 3, FullName = "Евгений Попов", Phone = "+79991000003", Vehicle = "Электросамокат", IsActive = true },
        new Courier { Id = 4, FullName = "Владимир Лебедев", Phone = "+79991000004", Vehicle = "Kia Rio", IsActive = true },
        new Courier { Id = 5, FullName = "Артем Козлов", Phone = "+79991000005", Vehicle = "Пешком", IsActive = true },
        new Courier { Id = 6, FullName = "Роман Новиков", Phone = "+79991000006", Vehicle = "Hyundai Solaris", IsActive = true },
        new Courier { Id = 7, FullName = "Денис Морозов", Phone = "+79991000007", Vehicle = "Велосипед", IsActive = true },
        new Courier { Id = 8, FullName = "Павел Соколов", Phone = "+79991000008", Vehicle = "Электросамокат", IsActive = true },
        new Courier { Id = 9, FullName = "Игорь Медведев", Phone = "+79991000009", Vehicle = "Пешком", IsActive = true },
        new Courier { Id = 10, FullName = "Александр Григорьев", Phone = "+79991000010", Vehicle = "Велосипед", IsActive = true }
    ];

    /// <summary>
    /// Создаёт список заказов
    /// </summary>
    /// <param name="customers">Список клиентов</param>
    /// <param name="restaurants">Список ресторанов</param>
    /// <param name="couriers">Список курьеров</param>
    /// <returns>Список заказов</returns>
    private static List<Order> GetOrders(List<Customer> customers, List<Restaurant> restaurants, List<Courier> couriers) =>
    [
        new Order { Id = 1, CustomerId = 2, RestaurantId = 1, CourierId = 1, Customer = customers[1], Restaurant = restaurants[0], Courier = couriers[0], DeliveryAddress = "ул. Ленина, д. 10", TotalAmount = 1500m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 2, CustomerId = 3, RestaurantId = 1, CourierId = 1, Customer = customers[2], Restaurant = restaurants[0], Courier = couriers[0], DeliveryAddress = "ул. Пушкина, д. 4", TotalAmount = 800m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.CardOnDelivery },
        new Order { Id = 3, CustomerId = 4, RestaurantId = 1, CourierId = 1, Customer = customers[3], Restaurant = restaurants[0], Courier = couriers[0], DeliveryAddress = "ул. Гагарина, д. 15", TotalAmount = 2500m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 4, CustomerId = 5, RestaurantId = 2, CourierId = 2, Customer = customers[4], Restaurant = restaurants[1], Courier = couriers[1], DeliveryAddress = "ул. Мира, д. 8", TotalAmount = 3000m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.Cash },
        new Order { Id = 5, CustomerId = 5, RestaurantId = 3, CourierId = 3, Customer = customers[4], Restaurant = restaurants[2], Courier = couriers[2], DeliveryAddress = "ул. Мира, д. 8", TotalAmount = 1200m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 6, CustomerId = 6, RestaurantId = 4, CourierId = 4, Customer = customers[5], Restaurant = restaurants[3], Courier = couriers[3], DeliveryAddress = "ул. Советская, д. 22", TotalAmount = 500m, Status = OrderStatus.Cancelled, PaymentMethod = PaymentMethod.CardOnDelivery },
        new Order { Id = 7, CustomerId = 7, RestaurantId = 2, CourierId = 2, Customer = customers[6], Restaurant = restaurants[1], Courier = couriers[1], DeliveryAddress = "ул. Кирова, д. 3", TotalAmount = 1800m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 8, CustomerId = 8, RestaurantId = 6, CourierId = 5, Customer = customers[7], Restaurant = restaurants[5], Courier = couriers[4], DeliveryAddress = "ул. Лесная, д. 11", TotalAmount = 900m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Cash },
        new Order { Id = 9, CustomerId = 9, RestaurantId = 2, CourierId = 2, Customer = customers[8], Restaurant = restaurants[1], Courier = couriers[1], DeliveryAddress = "ул. Новая, д. 7", TotalAmount = 2100m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 10, CustomerId = 10, RestaurantId = 10, CourierId = 6, Customer = customers[9], Restaurant = restaurants[9], Courier = couriers[5], DeliveryAddress = "ул. Парковая, д. 5", TotalAmount = 3500m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online }
    ];
}
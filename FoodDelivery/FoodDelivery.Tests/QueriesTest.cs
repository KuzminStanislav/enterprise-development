using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты запросов к данным службы доставки
/// </summary>
/// <param name="fixture">Тестовые данные</param>
public class QueriesTest(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    private readonly IReadOnlyList<Restaurant> _restaurants = fixture.Restaurants;
    private readonly IReadOnlyList<Order> _orders = fixture.Orders;

    /// <summary>
    /// Проверка получения клиентом рестораном
    /// </summary>
    [Fact]
    public void GetCustomersByRestaurant()
    {
        var targetRestaurantId = 1;
        var expectedCustomerNames = new[] 
        { 
            "Анна Иванова", 
            "Иван Лапшин", 
            "Максим Громов" 
        };

        var customers = _orders
        .Where(o => o.RestaurantId == targetRestaurantId)
        .Select(o => o.Customer)
        .DistinctBy(c => c.Id)
        .OrderBy(c => c.Name)
        .Select(c => c.Name)
        .ToList();

        Assert.Equal(expectedCustomerNames, resultCustomers);
    }

    /// <summary>
    /// Проверка получения идентификатора заказа
    /// </summary>
    [Fact]
    public void GetOrdersByStatus()
    {
        const OrderStatus targetStatus = OrderStatus.Delivering;
        var expectedOrderIds = new HashSet<int> 
        { 
            1, 3, 6, 8 
        };

        var resultOrdersIds = _orders
            .Where(o => o.Status == targetStatus)
            .Select(o => o.Id)
            .ToHashSet();

        Assert.True(
            expectedOrderIds.SetEquals(resultOrdersIds),
            $"Ожидались заказы [{string.Join(", ", expectedOrderIds)}], " +
            $"получены [{string.Join(", ", resultOrdersIds)}].");
    }

    /// <summary>
    /// Проверка подсчёта количества заказов для каждого ресторана
    /// </summary>
    [Fact]
    public void GetOrderCountByRestaurant()
    {
        var expectedRestaurantOrderCounts = new[]
        {
            new { RestaurantId = 1, OrderCount = 3 },
            new { RestaurantId = 2, OrderCount = 1 },
            new { RestaurantId = 3, OrderCount = 1 },
            new { RestaurantId = 4, OrderCount = 1 },
            new { RestaurantId = 5, OrderCount = 0 },
            new { RestaurantId = 6, OrderCount = 1 },
            new { RestaurantId = 7, OrderCount = 0 },
            new { RestaurantId = 8, OrderCount = 0 },
            new { RestaurantId = 9, OrderCount = 1 },
            new { RestaurantId = 10, OrderCount = 1 }
        };

        var restaurantOrderCounts = _restaurants
            .OrderBy(r => r.Id)
            .Select(r => new
            {
                Restaurant = r,
                OrderCount = _orders.Count(o => o.RestaurantId == r.Id)
            })
            .ToList();

        Assert.Equal(expectedRestaurantOrderCounts, restaurantOrderCounts);
    }

    /// <summary>
    /// Проверка получения топ-5 курьеров по количеству заказов
    /// </summary>
    [Fact]
    public void GetTopFiveCouriersByOrderCount()
    {
        var expectedTopCouriers = new[]
        {
            new { Courier = 1, OrderCount = 3 },
            new { Courier = 2, OrderCount = 3 },
            new { Courier = 3, OrderCount = 1 },
            new { Courier = 4, OrderCount = 1 },
            new { Courier = 5, OrderCount = 1 }
        };

        var topCouriers = _orders
            .Where(order => order.CourierId.HasValue)
            .GroupBy(order => order.CourierId.GetValueOrDefault())
            .Select(group => new 
            { 
                Courier = group.Key, 
                OrderCount = group.Count() 
            })
            .OrderByDescending(item => item.OrderCount)
            .ThenBy(item => item.Courier)
            .Take(5)
            .ToList();

        Assert.Equal(expectedTopCouriers, topCouriers);
    }

    /// <summary>
    /// Проверка получения топ-5 клиентов по суммарной стоимости заказов
    /// </summary>
    [Fact]
    public void GetTopFiveCustomersByTotalSpent()
    {
        var expectedTopCustomers = new[]
        {
            new { CustomerId = 5, TotalSpent = 4200m },
            new { CustomerId = 10, TotalSpent = 3500m },
            new { CustomerId = 9, TotalSpent = 2100m },
            new { CustomerId = 7, TotalSpent = 1800m },
            new { CustomerId = 2, TotalSpent = 1500m }
        };

        var topCustomers = _orders
            .GroupBy(order => order.Customer)
            .Select(group => new
            {
                CustomerId = group.Key.Id,
                TotalSpent = group.Sum(order => order.TotalAmount)
            })
            .OrderByDescending(item => item.TotalSpent)
            .Take(5)
            .ToList();

        Assert.Equal(expectedTopCustomers, topCustomers);
    }
}
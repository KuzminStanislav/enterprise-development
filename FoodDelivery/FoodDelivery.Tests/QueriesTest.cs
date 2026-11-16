using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Tests;

/// <summary>
/// Тесты запросов к данным службы доставки
/// </summary>
/// <param name="fixture">Тестовые данные</param>
public class QueriesTest : IClassFixture<QueriesTestFixture>
{
    private readonly List<Customer> _customers;
    private readonly List<Restaurant> _restaurants;
    private readonly List<Dish> _dishes;
    private readonly List<Courier> _couriers;
    private readonly List<Order> _orders;

    /// <summary>
    /// Инициализирует новый экземпляр класса с тестовыми данными
    /// </summary>
    /// <param name="fixture">Коллекция тестовых сущностей</param>
    public QueriesTest(QueriesTestFixture fixture)
    {
        _customers = fixture.Customers;
        _restaurants = fixture.Restaurants;
        _dishes = fixture.Dishes;
        _couriers = fixture.Couriers;
        _orders = fixture.Orders;
    }

    /// <summary>
    /// Вывод топ 5 ресторанов по количеству заказов
    /// </summary>
    [Fact]
    public void GetTopFiveRestaurantsByOrderCount()
    {
        var topRestaurants = _restaurants
            .Select(r => new
            {
                RestaurantId = r.Id,
                RestaurantName = r.Name,
                OrderCount = _orders.Count(o => o.RestaurantId == r.Id)
            })
            .OrderByDescending(x => x.OrderCount)
            .ThenBy(x => x.RestaurantId)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topRestaurants);
        Assert.True(topRestaurants.Count <= 5);
    }

    /// <summary>
    /// Вывод заказов с минимальным временем доставки
    /// </summary>
    [Fact]
    public void GetOrdersWithMinDeliveryTime()
    {
        var minDuration = _orders.Min(o => o.DeliveryTime - o.OrderTime);

        var fastOrders = _orders
            .Where(o => (o.DeliveryTime - o.OrderTime) == minDuration)
            .ToList();

        Assert.NotEmpty(fastOrders);
    }

    /// <summary>
    /// Вывод клиентов, сделавших заказ в выбранном ресторане
    /// </summary>
    [Fact]
    public void GetCustomersByRestaurantOrderedByName()
    {
        var targetRestaurantId = 1;

        var customers = _orders
            .Where(o => o.RestaurantId == targetRestaurantId && o.Customer != null)
            .Select(o => o.Customer!)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.Name)
            .ToList();

        var sortedNames = customers.Select(c => c.Name).ToList();
        var expectedNames = sortedNames.OrderBy(n => n).ToList();

        Assert.Equal(expectedNames, sortedNames);
    }

    /// <summary>
    /// 4. Вывод информации о заказах за определенный период
    /// </summary>
    [Fact]
    public void GetOrderSummaryByCategoryForPeriod()
    {
        var startDate = new DateTime(2025, 1, 1);
        var endDate = new DateTime(2027, 12, 31);

        var summary = _orders
            .Where(o => o.OrderTime >= startDate && o.OrderTime <= endDate)
            .SelectMany(o => _dishes.Where(d => d.RestaurantId == o.RestaurantId))
            .GroupBy(d => d.Category)
            .Select(g => new
            {
                Category = g.Key,
                OrderCount = g.Count(),
                AveragePrice = g.Average(d => d.Price),
                TotalPrice = g.Sum(d => d.Price)
            })
            .ToList();

        Assert.NotNull(summary);
    }

    /// <summary>
    /// 5. Вывод информации о клиенте, который потратил наибольшую сумму
    /// </summary>
    [Fact]
    public void GetCustomerWithMaxTotalSpent()
    {
        var topCustomerStat = _orders
            .Where(o => o.Customer != null)
            .GroupBy(o => o.Customer!)
            .Select(g => new
            {
                Customer = g.Key,
                TotalSpent = g.Sum(o => o.TotalAmount)
            })
            .OrderByDescending(x => x.TotalSpent)
            .FirstOrDefault();

        Assert.NotNull(topCustomerStat);
        Assert.NotNull(topCustomerStat.Customer);
    }
}
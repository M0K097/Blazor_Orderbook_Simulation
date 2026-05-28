using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Orderbook.Tests;

[TestClass]
public class OrderBookTests
{
    private AppDbContext db = null!;
    private OrderBook orderbook = null!;

    [TestInitialize]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        db = new AppDbContext(options);
        orderbook = new OrderBook(db);
    }

    [TestMethod]
    public void Test_OrderbookMatchmaking()
    {
        orderbook.place_limit_order(Side.sell, 10, 100);
        orderbook.place_limit_order(Side.buy, 10, 100);

        Assert.HasCount(1, orderbook.trade_log);

        var trade = orderbook.trade_log[0];

        Assert.AreEqual(10, trade.quantity);
        Assert.AreEqual(100, trade.execution_price);
    }

    [TestMethod]
    public void Test_ExecutionOrder()
    {
        orderbook.place_limit_order(Side.sell, 5, 100);
        orderbook.place_limit_order(Side.sell, 5, 101);

        orderbook.place_market_order(Side.buy, 5);

        Assert.HasCount(1, orderbook.trade_log);

        var trade = orderbook.trade_log[0];

        Assert.AreEqual(100, trade.execution_price);
        Assert.AreEqual(5, trade.quantity);
    }

    [TestMethod]
    public void Test_WritingDatabase()
    {
        orderbook.place_limit_order(Side.buy, 10, 100);

        Assert.AreEqual(1, db.Orders.Count());

        var order = db.Orders.First();

        Assert.AreEqual(10, order.quantity);
        Assert.AreEqual(Side.buy, order.order_side);
    }
}

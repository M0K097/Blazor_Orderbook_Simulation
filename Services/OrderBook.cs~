
public class OrderBook
{
    public List<LimitOrder> BIDS { get; private set; } = new List<LimitOrder>();
    public List<LimitOrder> ASKS { get; private set; } = new List<LimitOrder>();
    public List<Tradelog> trade_log = new List<Tradelog>();

    public event Action? trade_happened;

    public void place_limit_order(Side order_side, decimal quantity, decimal price)
    {
        var new_order = new LimitOrder(order_side, quantity, price);
        if (new_order.order_side == Side.buy)
            match_limit_order_buy(new_order);
        else
            match_limit_order_sell(new_order);
    }

    public void match_limit_order_sell(LimitOrder order)
    {
        while (order.status != Status.filled && BIDS.Count > 0)
        {
            var best_bid = BIDS[0];

            if (best_bid.price < order.price)
                break;
            trade(order, best_bid);
            if (best_bid.status == Status.filled)
                BIDS.Remove(best_bid);
        }
        if (order.status != Status.filled)
            insert_ask(order);
    }
    public void match_limit_order_buy(LimitOrder order)
    {
        while (order.status != Status.filled && ASKS.Count > 0)
        {
            var best_sell = ASKS[0];

            if (best_sell.price > order.price)
                break;
            trade(order, best_sell);
            if (best_sell.status == Status.filled)
                ASKS.Remove(best_sell);
        }
        if (order.status != Status.filled)
            insert_bid(order);
    }

    private void trade(Order o1, LimitOrder o2)
    {
        var qty = Math.Min(o1.remaining, o2.remaining);
        o1.fill(qty);
        o2.fill(qty);
        var log = new Tradelog(o1.order_id, o2.order_id, qty,o2.price);
        log.print();
        trade_log.Add(log);
        trade_happened?.Invoke();
    }

    public void insert_ask(LimitOrder order)
    {
        var i = 0;
        while (ASKS.Count > 0 && i < ASKS.Count && (ASKS[i].price < order.price ||
                ASKS[i].price == order.price && ASKS[i].order_id <= order.order_id))
        {
            i++;
        }
        ASKS.Insert(i, order);
    }

    public void insert_bid(LimitOrder order)
    {
        var i = 0;
        while (BIDS.Count > 0 && i < BIDS.Count && (BIDS[i].price > order.price ||
                BIDS[i].price == order.price && BIDS[i].order_id <= order.order_id))
        {
            i++;
        }
        BIDS.Insert(i, order);

    }
}

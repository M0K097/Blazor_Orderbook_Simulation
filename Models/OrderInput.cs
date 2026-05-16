public enum OrderType
{
    Limit,
    Market
}
public class OrderInput
{
    public OrderType Type {get;set;}
    public Side side {get;set;}

    public decimal Price {get;set;} = 1;

    public decimal Quantity {get;set;} = 1;
}


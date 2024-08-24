public interface IBuyable
{
    Buyable Buyable { get; }
}
public class Buyable : Item
{
    public int Cost { get; private set; }
    public bool Sellable { get; private set; }
    public int AmountStackable { get; private set; }
    public int BuyLimit { get; private set; }

    public void SetScriptableData(int id, int cost, bool sellable, int amountStackable, int buyLimit)
    {
        Id = id;
        Cost = cost;
        Sellable = sellable;
        AmountStackable = amountStackable;
        BuyLimit = buyLimit;
    }
}
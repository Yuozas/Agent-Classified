using UnityEngine;

[CreateAssetMenu(fileName = "Buyable", menuName = "BelongingsScriptable/Buyable", order = 1)]
public class BuyableScriptable : ScriptableObject
{
    public int id;
    public int cost;
    public bool sellable;
    public int amountStackable;
    public int buyLimit;
}
using UnityEngine;
[CreateAssetMenu(fileName = "Coin", menuName = "BelongingsScriptable/Coin", order = 1)]
public class CoinScriptable : ScriptableObject
{
    public GameObject prefab;
    public int price;
}

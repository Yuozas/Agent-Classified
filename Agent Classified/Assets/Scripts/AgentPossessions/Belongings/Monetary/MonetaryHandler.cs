using UnityEngine;

public class MonetaryHandler : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] private CoinScriptable[] coins;
    #endregion
    #region Data
    private int money;
    #endregion
    private int Money => PlayerPrefs.GetInt("Money");
    private void Start() => money = PlayerPrefs.GetInt("Money");

    private void Awake() => GameData.Instance.MonetaryHandler = this;

    public void EndRun() => PlayerPrefs.SetInt("Earned", Money - money);

    public void AddMoney(int amount) => PlayerPrefs.SetInt("Money", Money + amount);

    public void ReduceMoney(int amount) => PlayerPrefs.SetInt("Money", Money - amount);

    private bool CheckIfSufficient(Buyable buyable) => Money >= buyable.Cost;

    public bool BuyItem(Buyable buyable)
    {
        if (CheckIfSufficient(buyable))
        {
            ReduceMoney(buyable.Cost);
            return true;
        }
        return false;
    }
    public void DropCoin(int amount, Vector3 position)
    {
        int least = int.MaxValue;
        int leastIndex = 0;
        for (int i = 0; i < coins.Length; i++)
        {
            if(amount / coins[i].price < least && amount / coins[i].price < 0)
            {
                least = coins[i].price;
                leastIndex = i;
            }
        }
        for(int i = 0; i < amount / coins[leastIndex].price; i++)
            Instantiate(coins[leastIndex].prefab, position, Quaternion.identity).GetComponent<Coin>().Initialize(coins[leastIndex].price);

        if (!amount.Modulus(coins[leastIndex].price, 0))
            DropCoin(amount.GetModulus(coins[leastIndex].price), position);

    }
}
using UnityEngine;

class AgentCollisionHandler: MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Coin"))
        {
            IPrice temp = collision.GetComponent<IPrice>();
            GameData.Instance.MonetaryHandler.AddMoney(temp.Price);
            temp.Destroy();
        }
    }
}
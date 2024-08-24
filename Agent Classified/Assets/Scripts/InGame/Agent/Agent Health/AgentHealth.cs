using UnityEngine;
using UnityEngine.SceneManagement;

public class AgentHealth : MonoBehaviour
{
    #region Components

    [Header("Components")]
    [SerializeField] private GameObject heartPrefab;

    private GameObject[] heartSlots;

    #endregion Components

    #region Customizable

    [Header("Customizables")]
    [SerializeField] private int maximumHealth;

    #endregion Customizable

    #region Health data

    private int currentHealth;

    #endregion Health data

    private void Start()
    {
        currentHealth = maximumHealth;
        heartSlots = new GameObject[maximumHealth];
        for (int i = 0; i < maximumHealth; i++)
            heartSlots[i] = Instantiate(heartPrefab, new Vector3(-7.25f + 0.5f * i, 3.78f, 0), Quaternion.identity);
    }

    public void TakeDamage(IMobName mobName)
    {
        currentHealth--;
        for (int i = maximumHealth - 1; i >= 0; i--)
        {
            if (heartSlots[i].activeSelf)
            {
                heartSlots[i].SetActive(false);
                break;
            }
        }
        if (currentHealth == 0) Death(mobName.Name);
    }

    private void Death(string mobName)
    {

        PlayerPrefs.SetString("KilledBy", mobName);
        GameData.Instance.MonetaryHandler.EndRun();
        GameData.Instance.ScoreHandler.EndRun();
        PlayerPrefs.SetString("Menu", "Death Screen");
        SceneManager.LoadScene("Menu");
    }

}
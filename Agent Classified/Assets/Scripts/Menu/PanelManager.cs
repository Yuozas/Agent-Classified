using UnityEngine;

public class PanelManager : MonoBehaviour
{
    #region Components

    [Header("Components")]
    [SerializeField] private GameObject[] panels;

    #endregion Components

    private void Start()
    {
        if (PlayerPrefs.GetString("Menu").IsNullOrEmpty())
            PlayerPrefs.SetString("Menu", "Title Screen");

        SetMenu();
    }
    public void ChangeMenu(string menu)
    {
        PlayerPrefs.SetString("Menu", menu);
        SetMenu();
    }
    private void SetMenu()
    {
        for (int i = 0; i < panels.Length; i++)
        {
            if (panels[i].CompareTag(PlayerPrefs.GetString("Menu")))
                panels[i].SetActive(true);
            else
                panels[i].SetActive(false);
        }
    }
}
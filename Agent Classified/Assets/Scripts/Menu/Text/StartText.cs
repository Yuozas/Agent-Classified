using UnityEngine;
using UnityEngine.UI;

public enum PlayerPrefsType { Int = 0, String = 1, Float = 2}
public class StartText : MonoBehaviour
{
    #region Customizable
    [Header("Customizables")]
    [SerializeField] private string playerPrefsString;
    [SerializeField] private PlayerPrefsType playerPrefsType;
    #endregion
    #region Components

    private Text text;

    #endregion Components

    private void Awake() => text = GetComponent<Text>();

    private void Start() => text.text += GetPref();

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0066:Convert switch statement to expression", Justification = "<Pending>")]
    private string GetPref()
    {
        switch (playerPrefsType)
        {
            case PlayerPrefsType.Int:
                return $" {PlayerPrefs.GetInt(playerPrefsString)}";
            case PlayerPrefsType.String:
                return $" {PlayerPrefs.GetString(playerPrefsString)}";
            case PlayerPrefsType.Float:
                return $" {PlayerPrefs.GetFloat(playerPrefsString)}";
            default:
                return "";
        }
    }
}
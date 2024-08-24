using UnityEngine;
using UnityEngine.SceneManagement;

public class SwitchScene : MonoBehaviour
{
    public void Switch(string sceneName) => SceneManager.LoadScene(sceneName);
}
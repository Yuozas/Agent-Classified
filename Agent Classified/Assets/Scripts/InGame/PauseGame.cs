using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    private void Update()
    {
        switch (GameMode.Instance.InputMode)
        {
            case InputMode.Gamepad:
                GamePad();
                break;

            case InputMode.KeyboardMouse:
                KeyboardMouse();
                break;
        }
    }

    private void GamePad()
    {
        if (Input.GetKeyDown(KeyCode.JoystickButton9))
        {
            if (Time.timeScale.NotZero())
                Pause();
            else
                Resume();
        }
    }

    private void KeyboardMouse()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale.NotZero())
                Pause();
            else
                Resume();
        }
    }

    private void Pause()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }
}
using System.Collections;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    #region Components
    private GameMode gameMode;
    #endregion Components
    public GameObject[] WeaponPrefabs { private get; set; }
    public GameObject[] instantiatedPrefabs;
    public IPlayable[] playable;
    private int weaponInUse = 0;
    private void Awake()
    {
        AgentCommonData.Instance.WeaponHandler = this;
        gameMode = GameMode.Instance;
    }
    private void Start()
    {
        instantiatedPrefabs = new GameObject[WeaponPrefabs.Length];
        playable = new IPlayable[WeaponPrefabs.Length];
        for (int i = 0; i < WeaponPrefabs.Length; i++)
        {
            instantiatedPrefabs[i] = Instantiate(WeaponPrefabs[i], Vector3.zero, Quaternion.identity);
            instantiatedPrefabs[i].transform.parent = transform;
            playable[i] = instantiatedPrefabs[i].GetComponent<IPlayable>();
            instantiatedPrefabs[i].SetActive(true);
        }

    }

    private void SwitchWeapon(int index)
    {
        weaponInUse = index;
        playable[weaponInUse].CoolDown();
    }
    private void Update()
    {
        if (playable[weaponInUse].OnCooldown) return;
        WaitForWeaponSwitch();
        switch (gameMode.InputMode)
        {
            case InputMode.Gamepad:
                GamepadControl();
                break;

            case InputMode.KeyboardMouse:
                KeyboardMouseControl();
                break;
        }
    }
    private void WaitForWeaponSwitch()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
            SwitchWeapon(weaponInUse + 1 < WeaponPrefabs.Length ? weaponInUse + 1 : 0);
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            SwitchWeapon(weaponInUse - 1 >= 0 ? weaponInUse - 1 : WeaponPrefabs.Length - 1);
    }

    private void GamepadControl()
    {
        if (Input.GetKey(KeyCode.JoystickButton7)) playable[weaponInUse].Use();
    }

    private void KeyboardMouseControl()
    {
        if (Input.GetKey(KeyCode.Mouse0)) playable[weaponInUse].Use();
    }


}
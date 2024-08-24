using UnityEngine;

public class Crosshair : MonoBehaviour
{
    #region Components

    [Header("Components")]
    [SerializeField] private Transform agent;

    private GameMode gameMode;
    private Animator agentAnimator;

    #endregion Components

    #region Customizibles

    [Header("Customizables")]
    [SerializeField] private float joystickZoomSensitivity;

    #endregion Customizibles

    #region Crosshair Data

    private Vector2 cursorPos;
    private Vector2 oldCursorDirection;
    private float crosshairDistance = 1f;

    #endregion Crosshair Data

    #region Data for other components

    public Vector3 AimingDirection { get; private set; }

    #endregion Data for other components

    private void Awake()
    {
        AgentCommonData.Instance.Crosshair = this;
        gameMode = GameMode.Instance;
        agentAnimator = agent.GetComponent<Animator>();
    }

    private void Start() => Cursor.visible = false;

    private void Update()
    {
        switch (gameMode.InputMode)
        {
            case InputMode.Gamepad:
                GamepadControl();
                break;

            case InputMode.KeyboardMouse:
                KeyboardMouseControl();
                break;
        }

        transform.position = cursorPos;

        AimingDirection = transform.position.GetDirection(agent.position);
        agentAnimator.SetFloat("aimingX", AimingDirection.x);
        agentAnimator.SetFloat("aimingY", AimingDirection.y);
    }

    private void KeyboardMouseControl() => cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    private void GamepadControl()
    {
        if (Input.GetKey(KeyCode.JoystickButton6))
            crosshairDistance = Mathf.Clamp(crosshairDistance + joystickZoomSensitivity * Time.deltaTime, 1, 5);
        if (Input.GetKey(KeyCode.JoystickButton5))
            crosshairDistance = Mathf.Clamp(crosshairDistance - joystickZoomSensitivity * Time.deltaTime, 1, 5);

        Vector2 newCursorDirection;
        newCursorDirection.x = Input.GetAxisRaw("RightStickHorizontal");
        newCursorDirection.y = Input.GetAxisRaw("RightStickVertical") * -1;
        newCursorDirection.Normalize();
        oldCursorDirection = newCursorDirection != Vector2.zero ? newCursorDirection : oldCursorDirection;
        cursorPos = agent.position.Vector2() + oldCursorDirection.Multiply(crosshairDistance);
    }
}
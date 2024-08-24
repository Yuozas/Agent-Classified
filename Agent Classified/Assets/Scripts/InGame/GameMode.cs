public enum InputMode { KeyboardMouse, Gamepad }

public sealed class GameMode
{
    public InputMode InputMode { get; set; }

    private static readonly GameMode instance = new GameMode();

    static GameMode()
    {
    }

    private GameMode() => InputMode = InputMode.KeyboardMouse;

    public static GameMode Instance => instance;
}
public interface IPlayable
{
    void Use();
    void CoolDown();
    bool OnCooldown { get; }
}
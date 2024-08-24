public interface IWeapon
{
    Weapon Weapon { get; }
}
public class Weapon : Item
{
    public int Damage { get; private set; }
    public float UseCooldown { get; private set; }
    public float Force { get; private set; }
    public float KnockBackForce { get; private set; }
    internal void SetScriptableData(int id, int damage, float force, float useCooldown, float knockBackForce)
    {
        Id = id;
        Damage = damage;
        Force = force;
        UseCooldown = useCooldown;
        KnockBackForce = knockBackForce;
    }
}
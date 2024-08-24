using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "BelongingsScriptable/Weapon", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public int id;
    public int damage;
    public float useCooldown;
    public float force;
    public float knockBackForce;
}
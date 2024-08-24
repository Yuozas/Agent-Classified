using System.Collections;
using System.Linq;
using UnityEngine;

public class Shotgun : MonoBehaviour, IPlayable, IBuyable, IWeapon
{
    #region Define Data
    private readonly Weapon weapon = new Weapon();
    private readonly Buyable buyable = new Buyable();
    private BuyableScriptable buyableScriptable;
    private WeaponScriptable weaponScriptable;
    public Buyable Buyable => buyable;
    public Weapon Weapon => weapon;
    #endregion
    #region Components
    private Crosshair crosshair;
    private GameObject bullet;
    private Walking walking;
    #endregion

    #region Behaviour Data
    private bool onCooldown = false;
    #endregion
    private void SetItemData()
    {
        weapon.Id = 1;
        SetScriptables();
        SetScriptableData();
    }

    private void SetScriptables()
    {
        buyableScriptable = ItemCatalog.Instance.buyables.Where(b => b.id == weapon.Id).First();
        weaponScriptable = ItemCatalog.Instance.weapons.Where(w => w.id == weapon.Id).First();
    }

    private void SetScriptableData()
    {
        weapon.SetScriptableData(weaponScriptable.id, weaponScriptable.damage, weaponScriptable.force, weaponScriptable.useCooldown, weaponScriptable.knockBackForce);
        buyable.SetScriptableData(buyableScriptable.id, buyableScriptable.cost, buyableScriptable.sellable, buyableScriptable.amountStackable, buyableScriptable.buyLimit);
    }

    public void Start()
    {
        SetItemData();
        crosshair = AgentCommonData.Instance.Crosshair;
        walking = AgentCommonData.Instance.Agent.GetComponent<Walking>();
    }

    public void Awake()
    {
        bullet = (GameObject)Resources.LoadAll("Prefabs/Bullets", typeof(GameObject)).Where(g => g.name == "BulletDefault").FirstOrDefault();
    }
    public void Use()
    {
        if (onCooldown) return;

        StartCoroutine(ShootingCooldown());
        float spread = walking.IsWalking ? 0.03f : 0.1f;
        InstantiateBullet(spread, crosshair.AimingDirection.x, crosshair.AimingDirection.y);
        InstantiateBullet(spread * 3, crosshair.AimingDirection.x, crosshair.AimingDirection.y);
        InstantiateBullet(spread * 6, crosshair.AimingDirection.x, crosshair.AimingDirection.y);
    }
    public void CoolDown() => ShootingCooldown();
    public bool OnCooldown => onCooldown;
    private void InstantiateBullet(float spread, float x, float y) => Instantiate(bullet, transform.position, Quaternion.identity)
            .GetComponent<Bullet>().SetGet(weapon.Damage, 1, ((0f,3f),2f)).desiredVelocity
        .VelocityPrepareWithForce(Extensions.GetRandomDirection(x - spread, x + spread, y - spread, y + spread), weapon.Force);

    private IEnumerator ShootingCooldown(float? cooldown = null)
    {
        onCooldown.SetTrue();
        yield return new WaitForSeconds(cooldown.HasValue ? cooldown.Value : weapon.UseCooldown);
        onCooldown.SetFalse();
    }
}
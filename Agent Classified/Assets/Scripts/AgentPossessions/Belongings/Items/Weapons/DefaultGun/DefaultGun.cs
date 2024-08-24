using System.Collections;
using System.Linq;
using UnityEngine;

public class DefaultGun : MonoBehaviour, IPlayable, IWeapon
{

    #region Define Data
    private readonly Weapon weapon = new Weapon();
    private WeaponScriptable weaponScriptable;
    public Weapon Weapon => weapon;
    #endregion
    #region Components
    private Crosshair crosshair;
    private GameObject bullet;
    #endregion
    #region Behaviour Data
    private bool onCooldown = false;
    #endregion

    private void SetItemData()
    {
        weapon.Id = 0;
        SetScriptables();
        SetScriptableData();
    }

    private void SetScriptables() => weaponScriptable = ItemCatalog.Instance.weapons.Where(w => w.id == weapon.Id).First();

    private void SetScriptableData()
    {
        weapon.SetScriptableData(weaponScriptable.id, weaponScriptable.damage, weaponScriptable.force, weaponScriptable.useCooldown, weaponScriptable.knockBackForce);
    }

    public void Start()
    {
        SetItemData();
        crosshair = AgentCommonData.Instance.Crosshair;
    }

    public void Awake()
    {
        bullet = (GameObject)Resources.LoadAll("Prefabs/Bullets", typeof(GameObject)).Where(g => g.name == "BulletDefault").FirstOrDefault();
    }
    public void Use()
    {
        if (onCooldown) return;
        StartCoroutine(ShootingCooldown());
        InstantiateBullet();
    }
    public void CoolDown() => ShootingCooldown();
    public bool OnCooldown => onCooldown;
    private void InstantiateBullet() => Instantiate(bullet, transform.position, Quaternion.identity)
            .GetComponent<Bullet>().SetGet(weapon.Damage).desiredVelocity
        .VelocityPrepareWithForce(crosshair.AimingDirection, weapon.Force);

    private IEnumerator ShootingCooldown()
    {
        onCooldown.SetTrue();
        yield return new WaitForSeconds(weapon.UseCooldown);
        onCooldown.SetFalse();
    }
}

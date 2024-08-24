using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterBehaviour : MonoBehaviour, IDeathBehaviour, IMobName
{
    #region Customizables
    [Header("Customizables")]
    [SerializeField] float shootForce;
    [SerializeField] float cooldownTime;
    #endregion
    #region Data
    bool onCooldown;
    public string Name { get; set; }
    #endregion
    #region Components
    GameObject bullet;
    DamageEnemyCommunication damageEnemyCommunication;
    #endregion
    private void Awake()
    {
        bullet = transform.GetChild(1).gameObject;
        damageEnemyCommunication = GetComponent<DamageEnemyCommunication>();
    }
    private void Start() 
    {
        transform.Position(transform.position.MoveTowards(AgentCommonData.Instance.Agent.position));
        StartCoroutine(CoolDown());
    }
    void Update()
    {
        if (!onCooldown)
        {
            StartCoroutine(CoolDown());
            Shoot();
        }
    }
    IEnumerator CoolDown()
    {
        onCooldown.SetTrue();
        yield return new WaitForSeconds(cooldownTime);
        onCooldown.SetFalse();
    }
    void Shoot() => Instantiate(bullet, transform.position, Quaternion.identity).SetGetActive().GetComponent<MobBullet>().SetName(this).desiredVelocity
   .VelocityPrepareWithForce(AgentCommonData.Instance.Agent.position.GetDirection(transform.position), shootForce);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MobDamaging"))
            damageEnemyCommunication.DamageMob(collision.GetComponent<IDamage>().Damage);
    }

    public void Death()
    {
        StopAllCoroutines();
        GetComponent<Collider2D>().enabled = false;
        onCooldown.SetTrue();
    }
}

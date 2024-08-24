using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehaviour : MonoBehaviour, IDeathBehaviour, IMobName
{
    #region Components
    private readonly Transform Agent = AgentCommonData.Instance.Agent;
    private Rigidbody2D rb;
    private DamageEnemyCommunication damageEnemyCommunication;
    #endregion
    #region Customizable
    [Header("Customizables")]
    [SerializeField] private float followForce;
    #endregion
    #region Data
    private Vector2 desiredVelocity;
    public string Name { get ; set; }
    #endregion
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damageEnemyCommunication = GetComponent<DamageEnemyCommunication>();
    }

    // Update is called once per frame
    void Update()
    {
        PrepareVelocity();
        rb.Velocity(desiredVelocity);
    }

    public void PrepareVelocity() => desiredVelocity.VelocityPrepareWithForce(Agent.position.GetDirection(transform.position), followForce);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MobDamaging"))
            damageEnemyCommunication.DamageMob(collision.GetComponent<IDamage>().Damage);

        if (collision.isTrigger) return;
        if (collision.CompareTag("Agent"))
        {
            collision.GetComponent<AgentHealth>().TakeDamage(this);
            rb.Velocity();
            damageEnemyCommunication.Slam();
        }
    }

    public void Death() => GetComponent<Collider2D>().enabled = false;
}

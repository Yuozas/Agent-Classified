using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DasherBehaviour : MonoBehaviour, IDeathBehaviour, IMobName
{
    #region Components
    Rigidbody2D rb;
    private DamageEnemyCommunication damageEnemyCommunication;
    #endregion
    #region Data
    private Vector3 desiredVelocity;
    private System.Action updateMode;
    public string Name { get; set; }
    #endregion
    #region Customizable

    [Header("Customizable")]
    [SerializeField] private float shootingForce;
    [SerializeField] private float dashForce;
    #endregion Customizable
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damageEnemyCommunication = GetComponent<DamageEnemyCommunication>();
    }

    void Start() {
        desiredVelocity.VelocityPrepareWithForce(AgentCommonData.Instance.Agent.position.GetDirection(transform.position), shootingForce);
        updateMode = FirstMode;
    }
    private void Update() => updateMode.Invoke();
    private void FirstMode()
    {
        if(Vector2.Distance(transform.position, AgentCommonData.Instance.Agent.position) <= 2.3f)
        {
            updateMode = SecondMode;
            SecondModeTrigger();
            return;
        }
        rb.Velocity(desiredVelocity);
    }
    private void SecondModeTrigger()
    {
        Vector3 tempDirection = AgentCommonData.Instance.Agent.position.GetDirection(transform.position);
        desiredVelocity.VelocityPrepareWithForce(tempDirection + Extensions.GetRandomDirection(tempDirection.x - 1, tempDirection.x + 1, tempDirection.y - 1, tempDirection.y + 1).Vector3(), dashForce);
    }
    private void SecondMode()
    {
        rb.Velocity(desiredVelocity);
    }
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

    public void Death()
    {
        GetComponent<Collider2D>().enabled = false;
    }
}

using UnityEngine;

public class EyeballBehaviour : MonoBehaviour, IDeathBehaviour, IMobName
{
    #region Components

    private DamageEnemyCommunication damageEnemyCommunication;
    private Rigidbody2D rb;

    #endregion Components

    #region Customizable

    [Header("Customizable")]
    [SerializeField] private float shootingForce;

    #endregion Customizable

    #region Data
    Vector3 desiredVelocity;
    public string Name { get; set; }
    #endregion

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        damageEnemyCommunication = GetComponent<DamageEnemyCommunication>();
    }

    private void Start() => desiredVelocity.VelocityPrepareWithForce(AgentCommonData.Instance.Agent.position.GetDirection(transform.position), shootingForce);
    private void Update() => rb.Velocity(desiredVelocity);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("MobDamaging"))
        {
            damageEnemyCommunication.DamageMob(collision.GetComponent<IDamage>().Damage);
        }
        else if (collision.CompareTag("Agent"))
        {
            if (collision.isTrigger) return;
            collision.GetComponent<AgentHealth>().TakeDamage(this);
            rb.Velocity();
            damageEnemyCommunication.Slam();
        }
    }

    public void Death() => GetComponent<Collider2D>().enabled = false;
}
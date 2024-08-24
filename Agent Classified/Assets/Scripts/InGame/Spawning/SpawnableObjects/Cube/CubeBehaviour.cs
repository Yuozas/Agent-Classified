using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    #region Components

    private DamageEnemyCommunication damageEnemyCommunication;
    private Rigidbody2D rb;

    #endregion Components

    #region Customizable

    [Header("Customizable")]
    [SerializeField]
    private float shootingForce;

    #endregion Customizable

    #region Data
    Vector3 desiredVelocity;
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
            damageEnemyCommunication.DamageMob(1);
        }
        else if (collision.CompareTag("Agent"))
        {
            rb.Velocity();
            damageEnemyCommunication.Slam();
        }
    }
}
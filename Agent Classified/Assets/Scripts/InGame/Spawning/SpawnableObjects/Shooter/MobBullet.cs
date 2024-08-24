using System.Collections;
using UnityEngine;

public class MobBullet : MonoBehaviour
{
    #region Components
    Rigidbody2D rb;
    #endregion
    #region Data
    public Vector3 desiredVelocity;
    private IMobName MobName { get; set; }
    #endregion
    private void Awake() => rb = GetComponent<Rigidbody2D>();
    private void Start() => StartCoroutine(DespawnTimer());
    private void Update() => rb.Velocity(desiredVelocity);
    private IEnumerator DespawnTimer()
    {
        yield return new WaitForSeconds(5f);
        Despawn();
    }
    private void Despawn() => Destroy(gameObject);
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Agent"))
        {
            if (collision.isTrigger) return;
            collision.GetComponent<AgentHealth>().TakeDamage(MobName);
            rb.Velocity();
        }
    }
    public MobBullet SetName(IMobName mobName)
    {
        MobName = mobName;
        return this;
    }
}
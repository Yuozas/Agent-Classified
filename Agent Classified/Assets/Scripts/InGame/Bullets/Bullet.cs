using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour, IDamage
{
    #region Components
    Rigidbody2D rb;
    #endregion
    #region Data
    private Vector3 startPosition;
    public Vector2 desiredVelocity;
    private int defaultDamage;
    public float Damage { get; set; }
    private int stopsAfter;
    private ((float minDistance, float maxDistance) distance, float damageBonus)? distanceDamageBonus;
    #endregion
    private void Awake() => rb = GetComponent<Rigidbody2D>();
    private void Start() 
    {
        StartCoroutine(WaitDespawn());
        startPosition = transform.position;
    }
    private void Update() => rb.Velocity(desiredVelocity);
    public Bullet SetGet(int damage, int stopsAfter = -1, ((float minDistance, float maxDistance) distance, float damageBonus)? distanceDamageBonus = null)
    {
        Damage = damage;
        defaultDamage = damage;
        this.stopsAfter = stopsAfter;
        this.distanceDamageBonus = distanceDamageBonus;
        return this;
    }
    private IEnumerator WaitDespawn()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mob"))
        {
            BulletCollide();
            ApplyAdditionalDamage();
        }
    }
    private void BulletCollide()
    {
        if (stopsAfter > 0)
            stopsAfter--;
        if(stopsAfter == 0)
            Destroy(gameObject);
    }
    private void ApplyAdditionalDamage()
    {
        if (!distanceDamageBonus.HasValue) return;
        float distance = Vector2.Distance(transform.position, startPosition);
        float bonusPercent = Mathf.InverseLerp(distanceDamageBonus.Value.distance.maxDistance, distanceDamageBonus.Value.distance.minDistance, distance);
        Damage = defaultDamage + distanceDamageBonus.Value.damageBonus * bonusPercent;
    }
}
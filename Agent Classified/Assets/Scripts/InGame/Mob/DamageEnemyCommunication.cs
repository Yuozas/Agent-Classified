using System.Collections;
using UnityEngine;

public class DamageEnemyCommunication : MonoBehaviour
{
    #region Components

    private Spawner spawner;
    private HealthBarAnimation healthBarAnimation;
    private Animator animator;

    #endregion Components

    #region Customizable

    [Header("Customizable")]
    [SerializeField] private float despawnTime = 1f;

    #endregion Customizable

    #region Communication data

    private int spawnableObjectIndex;
    private int mobIndex;
    private float currentHealth;
    private int maxHealth;

    #endregion Communication data

    public void Initialize(Spawner spawner, int spawnableObjectIndex, int mobIndex)
    {
        this.spawner = spawner;
        this.spawnableObjectIndex = spawnableObjectIndex;
        this.mobIndex = mobIndex;
        maxHealth = spawner.GetHealth(spawnableObjectIndex);
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        healthBarAnimation = GetComponent<HealthBarAnimation>();
        animator = GetComponent<Animator>();
    }

    public void DamageMob(float damage)
    {
        if (currentHealth <= 0) return;

        currentHealth = currentHealth - damage < 0 ? 0 : currentHealth - damage;
        healthBarAnimation.ReceiveDamage(maxHealth, currentHealth);
        if (currentHealth == 0)
        {
            if(spawner.GetSpawnedObject(spawnableObjectIndex,mobIndex).TryGetComponent(out IDeathBehaviour deathBehaviour))
                deathBehaviour.Death();

            animator.SetTrigger("Death");
            spawner.Drop(spawnableObjectIndex, mobIndex);
            StartCoroutine(DespawnAfterAnimation(despawnTime));
        }
        else
            animator.SetTrigger("Damaged");
    }

    public void InstantDeath() => spawner.KillObject(spawnableObjectIndex, mobIndex);

    public void Slam()
    {
        animator.SetTrigger("Slam");
        StartCoroutine(DespawnAfterAnimation(0.25f));
    }

    private IEnumerator DespawnAfterAnimation(float despawnTime)
    {
        yield return new WaitForSeconds(despawnTime);
        spawner.KillObject(spawnableObjectIndex, mobIndex);
    }
}
using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour, IPrice
{
    #region Customizables

    [Header("Customizables")]
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float dropDuration = 3;
    [SerializeField] private Addon.Range<int> dropSpeed = new Addon.Range<int>(200, 500);
    [SerializeField] private float followAgentSpeed = 1000;

    #endregion Customizables

    #region Components

    private Rigidbody2D rb;

    #endregion Components
    #region Coin data
    public int Price { get; set; }
    #endregion
    private void Awake() => rb = GetComponent<Rigidbody2D>();
    public void Initialize(int price)
    {
        Price = price; 
        StartDrop();
    }
    public void StartDrop() => StartCoroutine(Drop());

    public void StartFollow() => StartCoroutine(FollowPlayer());

    private IEnumerator Drop()
    {
        Vector3 defaultVelocity = Extensions.GetRandomDirection();
        rb.Velocity(defaultVelocity);
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / dropDuration;
            rb.Velocity(defaultVelocity * Mathf.Lerp(dropSpeed.min, dropSpeed.min, animationCurve.Evaluate(time)).GetRound(3));
            yield return null;
        }
        StartFollow();
    }

    private IEnumerator FollowPlayer()
    {
        Transform agent = AgentCommonData.Instance.Agent;
        rb.Velocity(agent.position.GetDirection(transform.position) * followAgentSpeed);
        while (true)
        {
            if (Vector3.Distance(agent.position, transform.position) <= 0)
                break;
            rb.Velocity(agent.position.GetDirection(transform.position) * followAgentSpeed);
            yield return null;
        }
    }
    public void Destroy() => Destroy(gameObject);
}
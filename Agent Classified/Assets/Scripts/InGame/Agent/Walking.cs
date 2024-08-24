using UnityEngine;

public class Walking : MonoBehaviour
{
    #region Components

    private Rigidbody2D rb;
    private Animator animator;

    #endregion Components

    #region Customizables

    [Header("Customizables")]
    [SerializeField] private float walkingSpeed = 2;

    #endregion Customizables

    #region Agent Data

    private Vector2 walkingDirection;

    #endregion Agent Data

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start() => AgentCommonData.Instance.Agent = transform;

    private void Update()
    {
        WalkingInput();
        WalkingAnimation();
        WalkingPhysics();
    }

    private void WalkingInput() => walkingDirection.VelocityPrepareWithForce(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), walkingSpeed);

    private void WalkingAnimation()
    {
        animator.SetBool("walkingX", walkingDirection.x != 0);
        animator.SetBool("walkingY", walkingDirection.y != 0);
    }

    private void WalkingPhysics() => rb.Velocity(walkingDirection);
    public bool IsWalking => rb.velocity == Vector2.zero;
}
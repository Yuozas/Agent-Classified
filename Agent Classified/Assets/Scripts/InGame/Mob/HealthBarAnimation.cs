using UnityEngine;

public class HealthBarAnimation : MonoBehaviour
{
    #region Components

    public Transform healthBar;

    #endregion Components
    #region Health Data

    private float startScale;

    #endregion Health Data

    private void Awake() => healthBar = transform.GetChild(0);

    private void Start() => startScale = healthBar.localScale.x;

    public void ReceiveDamage(int maxHealth, float newHealth) => Reduce(startScale / maxHealth * newHealth);

    private void Reduce(float newXScale) => healthBar.localScale = healthBar.localScale.Amend(x: newXScale);
}
using Addon;
using System.Collections;
using UnityEngine;

public class UIZoomAnimation : MonoBehaviour
{
    #region Customizables

    [Header("Customizables")]
    [SerializeField] private int zoomUntil = 80;

    [SerializeField] private float zoomDuration = 3f;
    [SerializeField] private AnimationCurve animationCurve;

    #endregion Customizables

    #region Components

    private RectTransform rectTransform;

    #endregion Components

    #region Zoom Data

    private RangeVector3 range;

    #endregion Zoom Data

    private void Awake() => rectTransform = GetComponent<RectTransform>();

    private void Start()
    {
        range.max = rectTransform.localScale.Multiply(rectTransform.localScale.x);
        range.min = rectTransform.localScale.Multiply(range.max.x / 100 * zoomUntil);
        StartZoomOut();
    }

    private IEnumerator Zoom(System.Action onCompleted, RangeVector3 range)
    {
        float time = 0;
        while (time < 1)
        {
            time += Time.deltaTime / zoomDuration;
            float evaluatedTime = animationCurve.Evaluate(time);
            rectTransform.localScale = Vector3.Lerp(range.min, range.max, evaluatedTime).Round(3);
            yield return null;
        }

        onCompleted?.Invoke();
    }

    private void StartZoomOut() => StartCoroutine(Zoom(StartZoomIn, range.Flipped()));

    private void StartZoomIn() => StartCoroutine(Zoom(StartZoomOut, range));
}
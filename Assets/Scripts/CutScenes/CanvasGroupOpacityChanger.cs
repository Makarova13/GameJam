using UnityEngine;

public class CanvasGroupOpacityChanger : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AnimationCurve opacityCurve;
    [SerializeField] float duration = 1.0f;

    private float elapsedTime = 0.0f;
    private bool isAnimating = false;

    private void Awake()
    {
        canvasGroup.alpha = 0;
        Show();
    }

    void Update()
    {
        if (isAnimating)
        {
            elapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(elapsedTime / duration);
            float opacity = opacityCurve.Evaluate(normalizedTime);
            canvasGroup.alpha = opacity;

            if (normalizedTime >= 1.0f)
            {
                StopAnimation();
            }
        }
    }

    public void Show()
    {
        isAnimating = true;
        elapsedTime = 0.0f;
    }

    void StopAnimation()
    {
        isAnimating = false;
        elapsedTime = 0.0f;
    }
}

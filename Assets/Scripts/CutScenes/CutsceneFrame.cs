using UnityEngine;

public class CutsceneFrame : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AnimationCurve opacityCurve;
    [SerializeField] bool changeOpacity;
    [SerializeField] bool startWithOpacity;
    [SerializeField] float duration = 1.0f;
    [SerializeField] float showTime = 2f;

    public float ShowNextTime => showTime;

    private float elapsedTime = 0.0f;
    private bool isAnimating = false;

    private void Awake()
    {
        if (!startWithOpacity)
            canvasGroup.alpha = 0;
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

    public void ShowImidietly()
    {
        canvasGroup.alpha = 1;
    }

    public void Show()
    {
        if (changeOpacity)
        {
            isAnimating = true;
            elapsedTime = 0.0f;
        }
        else
        {
            canvasGroup.alpha = 1;
        }
    }

    void StopAnimation()
    {
        isAnimating = false;
        elapsedTime = 0.0f;
    }
}

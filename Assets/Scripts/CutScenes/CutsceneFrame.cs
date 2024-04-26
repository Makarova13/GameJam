using UnityEngine;

public class CutsceneFrame : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] AnimationCurve opacityCurve;
    [SerializeField] bool changeOpacity;
    [SerializeField] bool startWithOpacity;
    [SerializeField] float duration = 1.0f;
    [SerializeField] float showTime = 2f;
    [SerializeField] AudioClip clip;
    [SerializeField] AudioSource audioSource;
    [SerializeField] float audioVolume;
    [SerializeField] bool changeVolumeOverTime = true;

    public float ShowNextTime => showTime;

    private float elapsedTime = 0.0f;
    private float volumeElapsedTime = 0.0f;
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

        if (changeVolumeOverTime && audioVolume != audioSource.volume && volumeElapsedTime < showTime)
        {
            volumeElapsedTime += Time.deltaTime;
            float normalizedTime = Mathf.Clamp01(volumeElapsedTime / showTime);
            float newVolume = Mathf.Lerp(audioVolume, audioSource.volume, normalizedTime);

            audioSource.volume = newVolume;
        }
    }

    public void ShowImidietly()
    {
        canvasGroup.alpha = 1;
        changeVolumeOverTime = false;
        audioSource.volume = audioVolume;
    }

    public void Show()
    {
        volumeElapsedTime = 0;

        if (audioSource.clip != clip)
        {
            audioSource.clip = clip;
            audioSource.volume = audioVolume;
            audioSource.Play();
        }

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

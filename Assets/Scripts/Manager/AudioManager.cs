using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioLowPassFilter lowPassFilter;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void FadeToNormal(float delay, float normalCutoffFrequency, float fadeDuration)
    {
        StartCoroutine(FadeMusic(delay, normalCutoffFrequency, fadeDuration));
    }

    public void FadeToMuffled(float delay, float muffledCutoffFrequency, float fadeDuration)
    {
        StartCoroutine(FadeMusic( delay, muffledCutoffFrequency, fadeDuration));
    }

    private IEnumerator FadeMusic(float delay, float targetValue, float fadeDuration)
    {
        yield return new WaitForSeconds(delay);
        float currentTime = 0f;
        float startValue = lowPassFilter.cutoffFrequency;

        while (currentTime < fadeDuration)
        {
            currentTime += Time.deltaTime;
            float t = currentTime / fadeDuration;
            float newValue = Mathf.Lerp(startValue, targetValue, Mathf.SmoothStep(0f, 1f, t));
            lowPassFilter.cutoffFrequency = newValue;
            yield return null;
        }
    }
}

using UnityEngine;
using System.Collections;

public class SimpleFadeTransition : MonoBehaviour, IScreenTransition
{
    public float duration = 0.2f;

    public void PlayShowAnimation(GameObject screen, System.Action onComplete)
    {
        StartCoroutine(FadeIn(screen, onComplete));
    }

    public void PlayHideAnimation(GameObject screen, System.Action onComplete)
    {
        StartCoroutine(FadeOut(screen, onComplete));
    }

    private IEnumerator FadeIn(GameObject screen, System.Action onComplete)
    {
        CanvasGroup canvasGroup = screen.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = screen.AddComponent<CanvasGroup>();
        }
        canvasGroup.alpha = 0;
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(elapsed / duration);
            yield return null;
        }
        canvasGroup.alpha = 1;
        onComplete?.Invoke();
    }

    private IEnumerator FadeOut(GameObject screen, System.Action onComplete)
    {
        CanvasGroup canvasGroup = screen.GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = screen.AddComponent<CanvasGroup>();
        }
        float elapsed = 0;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Clamp01(1 - (elapsed / duration));
            yield return null;
        }
        canvasGroup.alpha = 0;
        onComplete?.Invoke();
    }
}
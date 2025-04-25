using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenManager : MonoBehaviour
{
    [SerializeField] private CanvasGroup loadingScreen;
    [SerializeField] private CanvasGroup introScreen;
    public float fadeSpeed = 1f;
    public float loadingDuration = 2f;

    private void Start()
    {
        StartCoroutine(HandleIntroSequence());
    }

    IEnumerator HandleIntroSequence()
    {

        loadingScreen.alpha = 1f;
        loadingScreen.blocksRaycasts = true;

 
        yield return new WaitForSeconds(loadingDuration);

        yield return StartCoroutine(FadeCanvasGroup(loadingScreen, 1f, 0f));

        loadingScreen.blocksRaycasts = false;

        introScreen.alpha = 0f;
        introScreen.blocksRaycasts = true;
        yield return StartCoroutine(FadeCanvasGroup(introScreen, 0f, 1f));
    }

    IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end)
    {
        float elapsed = 0f;
        while (elapsed < 1f)
        {
            elapsed += Time.deltaTime * fadeSpeed;
            cg.alpha = Mathf.Lerp(start, end, elapsed);
            yield return null;
        }
        cg.alpha = end;
    }
}

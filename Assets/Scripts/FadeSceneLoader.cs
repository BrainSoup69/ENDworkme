using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneLoader : MonoBehaviour
{

    [Header("Scene Load Settings")]
    public float waitTimeBeforeLoad = 1.5f;

    [Header("UI References")]
    public Animator loadingAnimator;

    private bool isLoading = false;

    public void LoadSceneWithFade(string sceneToLoad)
    {
        if (!isLoading)
        {
            StartCoroutine(LoadSceneRoutine(sceneToLoad));
        }
    }

    IEnumerator LoadSceneRoutine(string sceneToLoad)
    {
        isLoading = true;

        // Start fade animation
        if (loadingAnimator != null)
        {
            loadingAnimator.SetTrigger("EndFade");
        }

        yield return new WaitForSeconds(waitTimeBeforeLoad);

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false;

        while (asyncLoad.progress < 0.9f)
        {
            yield return null;
        }


        yield return new WaitForSeconds(0.5f);

        asyncLoad.allowSceneActivation = true;

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        if (loadingAnimator != null)
        {
            loadingAnimator.SetTrigger("StartFade");
        }

        isLoading = false;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeSceneLoader : MonoBehaviour
{
    public string sceneToLoad = "GameScene";           
    public Animator loadingAnimator;                
    public float waitTimeBeforeLoad = 1.5f;          

    public void LoadSceneWithFade()
    {
        StartCoroutine(LoadSceneRoutine());
    }

    IEnumerator LoadSceneRoutine()
    {
        if (loadingAnimator != null)
        {
            loadingAnimator.SetTrigger("StartFade");  
        }

        yield return new WaitForSeconds(waitTimeBeforeLoad); 

        SceneManager.LoadScene(sceneToLoad);
    }
}

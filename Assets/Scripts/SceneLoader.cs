using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
  
    public GameObject loadingScreen;

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadingScene(sceneName));
    }
    IEnumerator LoadingScene(string sceneName)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        AsyncOperation loadAsync = new AsyncOperation();
        loadAsync = SceneManager.LoadSceneAsync(sceneName);
        while (!loadAsync.isDone) yield return null;
        loadingScreen.SetActive(false);
        
    }
}

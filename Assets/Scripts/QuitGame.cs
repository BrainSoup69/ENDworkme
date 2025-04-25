using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
#if UNITY_EDITOR

        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Close the app in a build
        Application.Quit();
#endif
    }
}

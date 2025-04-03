using UnityEngine;
using DG.Tweening;  
public class ShowStats : MonoBehaviour
{

    [SerializeField] RectTransform destination;
  Vector3 offscreen;
    [SerializeField] RectTransform target;

    bool isActive=false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        offscreen = target.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            isActive = !isActive;
            if (isActive)
            {
                target.DOMoveX(destination.position.x, 0.3f);
            }
            else
            {
                target.DOMoveX(offscreen.x, 0.3f);
            }

        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class SongCreditSlide : MonoBehaviour
{
    public RectTransform creditPanel; 
    public float slideSpeed = 8f;
    public float displayTime = 3f;

    private Vector3 hiddenPos;
    private Vector3 visiblePos;
    private bool isSlidingIn = false;
    private bool isSlidingOut = false;
    private bool hasShown = false;

    private float timer;

    private void Start()
    {

        visiblePos = creditPanel.anchoredPosition;
        hiddenPos = new Vector3(Screen.width, visiblePos.y, 0);

        creditPanel.anchoredPosition = hiddenPos;
    }

    private void Update()
    {
        if (isSlidingIn)
        {
            creditPanel.anchoredPosition = Vector3.Lerp(creditPanel.anchoredPosition, visiblePos, Time.deltaTime * slideSpeed);
            if (Vector3.Distance(creditPanel.anchoredPosition, visiblePos) < 1f)
            {
                isSlidingIn = false;
                timer = displayTime;
            }
        }

        if (timer > 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                isSlidingOut = true;
            }
        }

        if (isSlidingOut)
        {
            creditPanel.anchoredPosition = Vector3.Lerp(creditPanel.anchoredPosition, hiddenPos, Time.deltaTime * slideSpeed);
            if (Vector3.Distance(creditPanel.anchoredPosition, hiddenPos) < 1f)
            {
                isSlidingOut = false;
            }
        }
    }

    public void ShowCredit()
    {
        if (hasShown) return;

        creditPanel.anchoredPosition = hiddenPos;
        isSlidingIn = true;
        isSlidingOut = false;
        hasShown = true;
    }

    public void ResetCredit()
    {
        hasShown = false;
    }
}

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class SongCreditSlide : MonoBehaviour
{
    public Transform creditPanel;
    public Transform offScreen, onScreen;
    public float slideSpeed = 8f;
    public float displayTime = 3f;


    private void Start()
    {
        creditPanel.position = offScreen.position;
        ShowCredits();
    }
  

    void ShowCredits()
    {
        Sequence tweenSeq = DOTween.Sequence() ;
        tweenSeq.Append( creditPanel.DOMoveX(onScreen.position.x, slideSpeed));
        tweenSeq.AppendInterval(displayTime);
        tweenSeq.Append(creditPanel.DOMoveX(offScreen.position.x, slideSpeed));


    }
   
}

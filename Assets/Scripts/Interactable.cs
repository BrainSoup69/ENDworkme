using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{

    public GameObject[] message;
    public bool loop = true;
    int currentMessage = 0;
    bool firstMessage = true;
  Outline outline=>GetComponent<Outline>();
    public UnityEvent onInteraction;


    public void Interaction()
    {
        onInteraction.Invoke();
    }

    public void EnableOutline()
    {
        outline.enabled = true;
    }
    public void DissableOutline()
    {
        outline.enabled = false;
    }
    public void ResetText()
    {
     if(currentMessage<message.Length)   message[currentMessage].SetActive(false);
        currentMessage = 0;
        firstMessage = true;
    }

    public void NextText()
    {
        if (firstMessage)
        {
            currentMessage = 0;
            message[0].SetActive(true);
            firstMessage = false;
        }
        else
        {
            if (currentMessage < message.Length) message[currentMessage].SetActive(false);
            currentMessage++;
            if (currentMessage >= message.Length && loop)
            {
                firstMessage = true;
                return;
            }
            if (currentMessage < message.Length) message[currentMessage].SetActive(true);
        }
    }
}

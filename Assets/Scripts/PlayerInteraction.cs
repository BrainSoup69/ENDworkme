using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{

    public float playerReach = 3f;
    Interactable currentInteractable;
 

 void Update()
    {

        CheckInteraction();
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            currentInteractable.Interaction();
            currentInteractable.NextText();
        }

    }

    void CheckInteraction()
    {
        if (Camera.main == null) return;

        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, playerReach))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
              
                Interactable newInteractable = hit.collider.GetComponent<Interactable>();
                if (newInteractable != null && newInteractable.enabled)
                {
                    SetNewCurrentInteractable(newInteractable);
                    currentInteractable.EnableOutline();
                }
            
            }
         
        }
        else
        {
            if (currentInteractable == null) return;
            currentInteractable.ResetText();
            currentInteractable.DissableOutline();
            currentInteractable = null;
        }
      
    }


    void SetNewCurrentInteractable(Interactable newInteractable)
    {

        currentInteractable = newInteractable;

    }

    
}

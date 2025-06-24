using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;
using UnityEngine.Events;

public class NPCDialogueWithItem : MonoBehaviour
{
    public bool needsItem = false;
    public string requiredItemName = "Item_Airhorn";
    public Animator animator;
    public DialogueWithEvent[] repeatingDialogue;
    public DialogueWithEvent[] dialogues;//zet hier in de inspector alle dialogue objecten in, ze worden 1 voor 1 aangezet.


    private Inventory playerInventory;
    private bool dialogueStarted = false;
    void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }

    public void CheckDialogue()
    {
        if (dialogueStarted) return;

        if (!needsItem)
        {
            StartCoroutine(PlayDialogue(dialogues));
        }
        else
        {
            if (playerInventory != null && playerInventory.HasItem(requiredItemName))
            {
                StartCoroutine(PlayDialogue(dialogues));
            }
            else
            {
                StartCoroutine(PlayDialogue(repeatingDialogue));
            }
        }

    }

    private IEnumerator PlayDialogue(DialogueWithEvent[] dialogue)
    {
        dialogueStarted = true;
        foreach (DialogueWithEvent dia in dialogue)
        {
            if (dia.hasAnimation)
            {
                animator?.SetTrigger(dia.animationTriggerID);
                yield return new WaitForSeconds(4f);
            }

            dia.dialogue?.SetActive(true);
            bool breakLoop = false;
            yield return new WaitForSeconds(0.3f);//wait a bit before detecting inputs.
            while (!breakLoop)
            {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Mouse0))
                {
                    breakLoop = true;
                }

                yield return null;
            }

            yield return new WaitForSeconds(0.2f);


            dia.dialogue?.SetActive(false);
            dia.dialogueEvent?.Invoke();
        }
        dialogueStarted = false;
    }
}

[Serializable]
public class DialogueWithEvent
{
    public GameObject dialogue;
    public UnityEvent dialogueEvent;
    public bool hasAnimation;
    public string animationTriggerID;
}
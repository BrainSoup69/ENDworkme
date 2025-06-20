using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
public class NPCDialogueWithItem : MonoBehaviour
{
    public GameObject[] normalDialogue;
    public GameObject[] specialDialogue;
    public string repeatLine;

    public string requiredItemName = "Item_Airhorn";
    public bool playAnimationWithItem = false;
    public Animator animator;
    public string normalAnimationTrigger;
    public string specialAnimationTrigger;


    public Text dialogueText;
    public GameObject dialogueUI;

    private bool isPlayerInRange = false;
    private bool hasHadNormalDialogue = false;
    private bool hasHadSpecialDialogue = false;

    private Inventory playerInventory;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }

    public void CheckDialogue()
    {

        if (playerInventory != null && playerInventory.HasItem(requiredItemName))
        {
            if (!hasHadSpecialDialogue)
            {
                //   GetComponent<PlayAnimationOnClick>().PlayAnimation(specialAnimationName);
                GameObject[] specialDialogue1 = specialDialogue;
                StartCoroutine(PlayDialogue(specialDialogue1, true, true));
                //     animator.SetTrigger(specialAnimationTrigger);

            }
            else
            {
                dialogueUI.SetActive(true);
                dialogueText.text = repeatLine;
            }
        }
        else
        {
            if (!hasHadNormalDialogue)
            {
                StartCoroutine(PlayDialogue(normalDialogue, false, false));
            }
            else
            {
                dialogueUI.SetActive(true);
                dialogueText.text = repeatLine;
            }
        }

    }

    private IEnumerator PlayDialogue(GameObject[] specialDialogue1, bool isSpecial, bool hasAnimation)
    {
        //  dialogueUI.SetActive(true);
        //Dit klopt nog niet denk ik. Maar heb de methoden even aangevuld op basis van de input.
        foreach (GameObject go in specialDialogue1)
        {
            go.SetActive(true);
            yield return new WaitForSeconds(2f);
            go.SetActive(false);
        }

        //  dialogueText.text = repeatLine;
        if (hasAnimation)
        {
            if (isSpecial) animator.SetTrigger(specialAnimationTrigger);
            else animator.SetTrigger(normalAnimationTrigger);
        }


        if (isSpecial) hasHadSpecialDialogue = true;
        else hasHadNormalDialogue = true;
    }

    private IEnumerator PlayDialogue(string[] lines, bool isSpecial, bool hasAnimation)
    {
        dialogueUI.SetActive(true);

        foreach (string line in lines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(2f);
        }

        dialogueText.text = repeatLine;

        if (hasAnimation)
        {
            if (isSpecial) animator.SetTrigger(specialAnimationTrigger);
            else animator.SetTrigger(normalAnimationTrigger);
        }


        if (isSpecial) hasHadSpecialDialogue = true;
        else hasHadNormalDialogue = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            dialogueUI.SetActive(false);
        }
    }
}

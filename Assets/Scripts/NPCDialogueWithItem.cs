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

    public Text dialogueText;
    public GameObject dialogueUI;
    public Animator npcAnimator;

    private bool isPlayerInRange = false;
    private bool hasHadNormalDialogue = false;
    private bool hasHadSpecialDialogue = false;

    private Inventory playerInventory;

    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerInventory = player.GetComponent<Inventory>();
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (playerInventory != null && playerInventory.HasItem(requiredItemName))
            {
                if (!hasHadSpecialDialogue)
                {
                    npcAnimator.SetTrigger("TalkSpecial");
                    GameObject[] specialDialogue1 = specialDialogue;
                    StartCoroutine(PlayDialogue(specialDialogue1, true));
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
                    StartCoroutine(PlayDialogue(normalDialogue, false));
                }
                else
                {
                    dialogueUI.SetActive(true);
                    dialogueText.text = repeatLine;
                }
            }
        }
    }

    private IEnumerator PlayDialogue(GameObject[] specialDialogue1, bool v)
    {
        throw new NotImplementedException();
    }

    private IEnumerator PlayDialogue(string[] lines, bool isSpecial)
    {
        dialogueUI.SetActive(true);

        foreach (string line in lines)
        {
            dialogueText.text = line;
            yield return new WaitForSeconds(2f);
        }

        dialogueText.text = repeatLine;

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

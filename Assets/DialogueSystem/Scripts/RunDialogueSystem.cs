using UnityEngine;
using TMPro;

public class RunDialogueSystem : MonoBehaviour
{
    [SerializeField] DialogueSystemSO dialogueChain;
    [SerializeField] TMP_Text textField;
    DialogueObject currentDialogue;
    void Awake()
    {
        currentDialogue = dialogueChain.GetDialogue();
        textField.SetText(currentDialogue.GetDialogue());
    }
    public void NextDialogue(int index)
    {
        if (currentDialogue.NextDialogue(index) == null) return;
        currentDialogue = currentDialogue.NextDialogue(index);
        textField.SetText(currentDialogue.GetDialogue());

    }
}

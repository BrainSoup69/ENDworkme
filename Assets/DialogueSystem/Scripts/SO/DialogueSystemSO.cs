using System;
using UnityEngine;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Data", menuName = "Dialogue/DialogueItem", order = 1)]

public class DialogueSystemSO : ScriptableObject
{
    [SerializeField] DialogueObject dialogueChain;

    public DialogueObject GetDialogue()
    {
        return dialogueChain;
    }


}

[Serializable]
public class DialogueObject
{
    [TextArea][SerializeField] string dialogue;
    [SerializeField] DialogueObject[] nextDialogue;

    public DialogueObject NextDialogue(int nextIndex)
    {
        if (nextDialogue.Length == 0) return null;
        if (nextDialogue[nextIndex] != null) return nextDialogue[nextIndex];
        else return null;
    }
    public string GetDialogue()
    {
        return dialogue;
    }
}

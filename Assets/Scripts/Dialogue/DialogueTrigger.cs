using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    //Object that contains the dialogue
    public Dialogue dialogue;

    //Function that triggers the actual dialogue to begin
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

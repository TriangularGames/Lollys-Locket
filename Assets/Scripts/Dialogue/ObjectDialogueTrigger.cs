using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Trigger", menuName = "Inventory/Dialogue Trigger")]
public class ObjectDialogueTrigger : ScriptableObject
{
    //Object that contains the dialogue
    public Dialogue dialogue;

    //Function that triggers the actual dialogue to begin
    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

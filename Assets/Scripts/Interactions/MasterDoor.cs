using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MasterDoor : DoorInteraction
{
    private DialogueManager dialoguemanager;
    public ObjectDialogueTrigger dialogue;

    void Start()
    {
        dialoguemanager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public void missingkey()
    {
        dialogue.TriggerDialogue();
    }
}

using System.Collections;
using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    public GameObject toDoor;
    public GameObject Player;
    public Animator transition;
    private float transitionTime = 2f;
    public ObjectDialogueTrigger dialogue;
    private DialogueManager dialoguemanager;
    private bool triggered;
    private bool done;

    private void Start()
    {
        dialoguemanager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }

    public void goIn()
    {
        triggered = true;
        dialogue.TriggerDialogue();
    }

    private void Update()
    {
        if (dialoguemanager.Finished && triggered && !done)
        {
            StartCoroutine("goTo");
            done = true;
        }
    }

    IEnumerator goTo()
    {
        //Start Transition
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        
        Player.transform.position = toDoor.transform.position;
        
        //Finish transition
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);


    }
}

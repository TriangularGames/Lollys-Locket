using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    //Interaction variables
    public Transform detectionPoint;
    private const float detectionRadius = 1f;
    public LayerMask detectionLayer;
    public GameObject detectedObject;
    private string objType;
    private DialogueManager dialoguemanager;
    public bool interacting;
    private DialogueTrigger interactable;
    private bool dialogueTriggered;


    //Final Strech triggers
    public Item Locket;
    public GameObject Vampir;
    public bool vampirTrigger;
    private bool Vampire;

    //Door variables for chase
    public GameObject ExitDoor;
    public GameObject pastDoor;

    //Dying/Endgame stuff
    public GameObject panel;
    public GameObject endScreen;
    private bool end;

    private void Start()
    {
        //Get the dialogue manager for ease of access
        dialoguemanager = GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>();
    }


    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                interacting = true;
                if (objType == "Dialogue")
                {
                    dialogueTriggered = true;
                    if (detectedObject.tag == "Locket")
                    {
                        vampirTrigger = true;
                    }
                    interactable = detectedObject.GetComponent<DialogueTrigger>();
                    interactable.TriggerDialogue();
                }
                else if (objType == "Exit")
                {
                    if (detectedObject.tag == "Exit" && Vampire && !end)
                    {
                        DoAction("Leave");
                        end = true;
                        interacting = false;
                    }
                }
                else if (objType == "Door")
                {
                    if (detectedObject.name == "Master Door")
                    {
                        if (Inventory.instance.items.Contains(detectedObject.GetComponent<MasterDoor>().neededitem))
                        {
                            detectedObject.GetComponent<MasterDoor>().go();
                            interacting = false;
                        }
                        else
                        {
                            dialogueTriggered = true;
                            detectedObject.GetComponent<MasterDoor>().missingkey();
                        }
                    }
                    if (detectedObject.tag == "Door" && Vampire)
                    {
                        detectedObject.GetComponent<DoorInteraction>().goToDoor();
                        pastDoor = detectedObject.GetComponent<DoorInteraction>().toDoor;
                        interacting = false;
                    }
                    else
                    {
                        detectedObject.GetComponent<DoorInteraction>().go();
                        interacting = false;
                    }
                }
            }
        }
        //Check if the dialogue is done to do action
        if (interacting && dialogueTriggered && dialoguemanager.Finished)
        {
            //If there was an action triggered
            if (dialoguemanager.actionTriggered)
            {
                //Take care of it
                DoAction(dialoguemanager.actionTaken);
            }
            //Make interacting false
            interacting = false;
            dialogueTriggered = false;
        }

        if (dialoguemanager.Finished && vampirTrigger)
        {
            Vampir.SetActive(true);
            vampirTrigger = false;
            Vampire = true;
            interacting = false;
        }
    }

    bool InteractInput()
    {
        if (!interacting)
        {
            return Input.GetKeyDown(KeyCode.E);
        }
        else
        {
            return false;
        }
        
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if (obj == null)
        {
            return false;
        }
        else if (obj.GetComponent<DialogueTrigger>())
        {
            detectedObject = obj.gameObject;
            objType = "Dialogue";
            return true;
        }
        else if (obj.tag == "Door")
        {
            detectedObject = obj.gameObject;
            objType = "Door";
            return true;
        }
        else if (obj.tag == "Exit")
        {
            detectedObject = obj.gameObject;
            objType = "Exit";
            return true;
        }
        else
        {
            return false;
        }
    }

    void DoAction(string action)
    {
        switch (action) //Check which action will be taken
        {
            case "Pickup": //Pickup an item
                Item pickedup = interactable.dialogue.pickUp;
                Inventory.instance.Add(pickedup);
                Destroy(interactable.gameObject);
                interacting = false;
                interactable = null;
                break;

            case "PlaceHead":
                if (dialoguemanager.selectionsSkipped)
                {
                    interacting = false;
                    interactable = null;
                    break;
                }
                else
                {
                    Inventory.instance.Remove(interactable.dialogue.pickUp);
                    GameObject.FindGameObjectWithTag("DollPuzzle").GetComponent<Doll>().PlaceHead(interactable.dialogue.pickUp);
                    interacting = false;
                    interactable = null;
                    break;
                }

            case "PlacePerson":
                if (dialoguemanager.selectionsSkipped)
                {
                    interacting = false;
                    interactable = null;
                    break;
                }
                else
                {
                    Inventory.instance.Remove(interactable.dialogue.pickUp);
                    GameObject.FindGameObjectWithTag("DiningRoomPuzzle").GetComponent<DiningRoom>().PlaceObject(interactable.dialogue.pickUp);
                    interacting = false;
                    interactable = null;
                    break;
                }

            case "Kill": //Fucking P E R I S H
                GameOver();
                StopAllCoroutines();
                interacting = false;
                interactable = null;
                break;

            case "Unlock":
                if (dialoguemanager.selectionsSkipped)
                {
                    interacting = false;
                    interactable = null;
                    break;
                }
                else
                {
                    GameObject.FindGameObjectWithTag("SecretDoor").GetComponent<SecretDoor>().goIn();
                    interacting = false;
                    interactable = null;
                    break;
                }

            case "Leave":
                if (Inventory.instance.items.Contains(Locket))
                {
                    interactable = null;
                    interacting = false;
                    Destroy(Vampir);
                    TriggerEnd(); //End the game!
                }
                break;

            default:
                interacting = false;
                interactable = null;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D other) //Check if player is in range
    {
        if (other.gameObject.name == "InFoyer")
        {
            GetComponent<PlayerMovement>().isInFoyer = true;
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) //Kill if she touches enemies
     {
         if(other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            DoAction("Kill");
        }
     } 

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "InFoyer")
        {
            GetComponent<PlayerMovement>().isInFoyer = false;
        }
        interactable = null;
    }

    private void GameOver() { //Game Over
        panel.SetActive(true);
    }

    void TriggerEnd()
    {
        Destroy(GameObject.FindGameObjectWithTag("Thunder"));
        endScreen.SetActive(true);
        endScreen.GetComponent<EndStory>().displayStory();
    }


}

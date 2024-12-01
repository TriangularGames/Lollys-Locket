using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    // Objects for dialogue window
    public TextMeshProUGUI dialogueBox;
    public GameObject dialogueWindow;

    //Speed of dialogue
    public float delay = 0.1f;

    //Variables for typing
    private string currentSentence;
    private bool isTyping;

    private Coroutine lastRoutine = null;

    //Variables for selection window Text stuff
    private bool selections;
    private string[] options;
    private string[] uponSelect;

    //Checks if the selection window is active
    private bool selectionsActive;

    //Checks which element the action will be taken on
    private int actionElement;

    //Variables for triggering the actual action from selection
    public bool Finished;
    public bool actionTriggered;
    public string actionTaken;

    //Selection window object variables
    public GameObject selectionWindow;
    public GameObject optionA;
    public GameObject optionB;
    public TextMeshProUGUI Atext;
    public TextMeshProUGUI Btext;

    //Alt dialogue if object is missing
    private Item ObjectToPlace;
    private string[] altDialogue;
    public bool selectionsSkipped;
    private bool altOccured;

    //Object that keeps track of sentences in the given dialogue
    private Queue<string> sentences;

    //Sets the window to inactive on start and sets the sentences to a new queue
    void Start()
    {
        sentences = new Queue<string>();
        dialogueWindow.SetActive(false);
    }

    //Update for doing actions with the Selection Window
    private void Update()
    {
        if (selectionsActive)
        {
            //Move selection up
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                //Makes sure you are at the lowest selection before moving up
                if (optionB.activeSelf)
                {
                    optionB.SetActive(false);
                    optionA.SetActive(true);
                }
            }

            //Move selection down
            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                //Makes sure you are at the highest selection before moving down
                if (optionA.activeSelf)
                {
                    optionA.SetActive(false);
                    optionB.SetActive(true);
                }
            }

            //Making selection
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Checks which option was selected
                if (optionA.activeSelf)
                {
                    //Gets the consequence text
                    string selected = uponSelect[0];

                    //If this is the element that triggers the action it says so
                    if (actionElement == 0)
                    {
                        actionTriggered = true;
                    }

                    //Displays text for option
                    DisplaySelection(selected);
                }
                else if (optionB.activeSelf)
                {
                    //Gets the consequence text
                    string selected = uponSelect[1];

                    //If this is the elemeny that triggers the action it says so
                    if (actionElement == 1)
                    {
                        actionTriggered = true;
                    }

                    //Displays text for option
                    DisplaySelection(selected);
                }
            }
        }
    }

    //Displays dialogue selections if there are any
    private void DisplayOptions()
    {
        Atext.text = options[0];
        Btext.text = options[1];
        optionA.SetActive(true);
        optionB.SetActive(false);
        selectionsActive = true;
        selectionWindow.SetActive(true);
    }

    //Displays the selected option dialogue
    public void DisplaySelection(string selected)
    {
        dialogueBox.text = "";
        selectionWindow.SetActive(false);
        selectionsActive = false;
        selections = false;
        sentences.Clear();
        currentSentence = selected;
        lastRoutine = StartCoroutine(TypeText(selected));
    }


    //When an object is interacted with, the Dialogue box will appear
    public void StartDialogue(Dialogue dialogue)
    {
        ObjectToPlace = null;
        selectionsSkipped = false;
        altOccured = false;
        Finished = false;
        isTyping = false;

        //Add stuff to dialogue UI
        dialogueWindow.SetActive(true);

        sentences.Clear();

        //Checks for selection window variables
        selections = dialogue.hasOptions;
        options = dialogue.selections;
        uponSelect = dialogue.OptionDialogue;
        actionElement = dialogue.interactionOption;
        actionTaken = dialogue.Action;

        if (!Inventory.instance.items.Contains(dialogue.pickUp) && actionTaken == "Unlock")
        {
            ObjectToPlace = dialogue.pickUp;
            selectionsSkipped = true;
        }

        if ((actionTaken == "PlaceHead" || actionTaken == "PlacePerson") && !Inventory.instance.items.Contains(dialogue.pickUp))
        {
            ObjectToPlace = dialogue.pickUp;
            selectionsSkipped = true;
        }

        altDialogue = dialogue.noItem;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    //This is triggered when the continue button on the Dialogue box is pressed
    public void DisplayNextSentence()
    {
        if (!isTyping)
        {
            //If there are no selections, end dialogue
            if (sentences.Count == 0 && (!selections || (selectionsSkipped && altOccured)))
            {
                EndDialogue();
                return;
            }
            //Otherwise display selections
            else if (sentences.Count == 0 && selections)
            {
                if (ObjectToPlace != null && !(Inventory.instance.items.Contains(ObjectToPlace)))
                {
                    altOccured = true;
                    dialogueBox.text = "";

                    foreach (string sentence in altDialogue)
                    {
                        sentences.Enqueue(sentence);
                    }
                    currentSentence = sentences.Dequeue();
                    StopCoroutine(lastRoutine);
                    lastRoutine = StartCoroutine(TypeText(currentSentence));

                }
                else
                {
                    DisplayOptions();
                }
            }
            //If dialogue isn't done, continue writing the sentences
            else if (!selectionsSkipped)
            {
                dialogueBox.text = "";
                currentSentence = sentences.Dequeue();
                lastRoutine = StartCoroutine(TypeText(currentSentence));
            }
            else if (selectionsSkipped)
            {
                dialogueBox.text = "";
                currentSentence = sentences.Dequeue();
                sentences.Clear();
                lastRoutine = StartCoroutine(TypeText(currentSentence));
            }
        }
    }

    //Function for skipping the typing of dialogue
    public void Skip()
    {
        if (isTyping)
        {
            StopCoroutine(lastRoutine);
            dialogueBox.text = "";
            dialogueBox.text = currentSentence;
            StartCoroutine("WaitToRead");
            isTyping = false;
        }
    }

    //If dialogue is skipped, allow for it to be read
    IEnumerator WaitToRead()
    {
        yield return new WaitForSeconds(1f);
    }

    //Function to type the text out
    IEnumerator TypeText(string sentence)
    {
        isTyping = true;
        foreach (char letter in sentence)
        {
            dialogueBox.text += letter;
            yield return new WaitForSeconds(delay);
        }
        isTyping = false;
    }

    //When there is no sentences left, or the player interacts with an object, the Dialogue box will disappear
    void EndDialogue()
    {
        Finished = true;
        dialogueBox.text = "";
        dialogueWindow.SetActive(false);
    }

}

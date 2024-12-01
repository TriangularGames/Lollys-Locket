using UnityEngine;

[System.Serializable]
public class Dialogue 
{
    //Item to pick up (if there is one)
    public Item pickUp;

    //The particular action agreeing to interact will cause
    public string Action;

    //The specific option that triggers the given action
    public int interactionOption;

    //If there is a possible interaction
    public bool hasOptions;

    //Sentences for the dialogue itself
    [TextArea(1, 10)]
    public string[] sentences;

    //Options for interaction
    [TextArea(1, 1)]
    public string[] selections;

    //The consequence dialogue after the chosen interaction
    [TextArea(1, 10)]
    public string[] OptionDialogue;

    //Dialogue if the object to place/give isnt in the inventory
    [TextArea(1, 10)]
    public string[] noItem;
    
}

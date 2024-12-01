using UnityEngine;

public class DiningRoom : MonoBehaviour
{
    //Chair objects
    public GameObject chair1;
    public GameObject chair2;
    public GameObject chair3;
    public GameObject chair4;
    public GameObject chair5;
    public GameObject chair6;

    //Displayed Objects
    public GameObject chairA;
    public GameObject chairB;
    public GameObject chairC;
    public GameObject chairD;
    public GameObject chairE;
    public GameObject chairF;

    //What item needs to be placed in the respective chair
    private string object1;
    private string object2;
    private string object3;
    private string object4;
    private string object5;
    private string object6;

    //If the correct item is placed
    private bool item1;
    private bool item2;
    private bool item3;
    private bool item4;
    private bool item5;
    private bool item6;

    public ObjectDialogueTrigger trigger;
    public GameObject secretkey;

    private bool done;


    // Start is called before the first frame update
    void Start()
    {
        object1 = "Skeleton";
        object2 = "Bowtie-ton";
        object3 = "Broken Skull";
        object4 = "Hay?";
        object5 = "Headless Doll";
        object6 = "Eyeballs";
    }

    // Update is called once per frame
    void Update()
    {
        //Checks if all items have been placed
        if (item1 && item2 && item3 && item4 && item5 && item6 && !done)
        {
            secretkey.SetActive(true);
            trigger.TriggerDialogue();
            done = true;
        }
    }

    public void PlaceObject(Item item)
    {

        if (item.name == object1) //&& player is interacting with said chair
        {
            chairA.SetActive(true);
            item1 = true;
            Destroy(chair1.GetComponent<DialogueTrigger>());
        }
        else if (item.name == object2)
        {
            chairB.SetActive(true);
            item2 = true;
            Destroy(chair2.GetComponent<DialogueTrigger>());
        }
        else if (item.name == object3)
        {
            chairC.SetActive(true);
            item3 = true;
            Destroy(chair3.GetComponent<DialogueTrigger>());
        }
        else if (item.name == object4)
        {
            chairD.SetActive(true);
            item4 = true;
            Destroy(chair4.GetComponent<DialogueTrigger>());
        }
        else if (item.name == object5)
        {
            chairE.SetActive(true);
            item5 = true;
            Destroy(chair5.GetComponent<DialogueTrigger>());
        }
        else if (item.name == object6)
        {
            chairF.SetActive(true);
            item6 = true;
            Destroy(chair6.GetComponent<DialogueTrigger>());
        }
    }
}

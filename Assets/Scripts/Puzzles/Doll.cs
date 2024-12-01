using UnityEngine;

public class Doll : MonoBehaviour
{
    public GameObject doll1;
    public GameObject doll2;

    public Sprite doll1Fixed;
    public Sprite doll2Fixed;

    private string head1;
    private string head2;

    private bool has1;
    private bool has2;

    public GameObject note;
    public ObjectDialogueTrigger trigger;
    private bool notDone;

    // Start is called before the first frame update
    void Start()
    {
        head2 = "Molly's Head";
        head1 = "Alice's Head";
    }

    // Update is called once per frame
    void Update()
    {
        if (has1 && has2 && !notDone)
        {
            note.SetActive(true);
            trigger.TriggerDialogue();
            notDone = true;

        }
    }

    public void PlaceHead(Item item)
    {
        if (item.name == head1) //&& player is interacting with said doll
        {
            Inventory.instance.Remove(item);
            doll1.GetComponent<SpriteRenderer>().sprite = doll1Fixed;
            has1 = true;
        }
        else if (item.name == head2) //&& player is interacting with said doll
        {
            Inventory.instance.Remove(item);
            doll2.GetComponent<SpriteRenderer>().sprite = doll2Fixed;
            has2 = true;
        }
    }
}

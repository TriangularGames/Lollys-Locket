using UnityEngine;

public class EndGame : MonoBehaviour
{
    private bool complete;
    public Item MasterKey;
    public Sprite death;
    // Update is called once per frame
    void Update()
    {
        if (Inventory.instance.items.Contains(MasterKey) && !complete)
        {
            GetComponent<SpriteRenderer>().sprite = death;
            complete = true;
        }
    }
}

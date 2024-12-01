using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    Item item;
    public GameObject icon;
    public TextMeshProUGUI description;
    public TextMeshProUGUI nameDisplay;
    private TextMeshProUGUI itemName;
    private GameObject inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag("Inventory");
        itemName = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1) && item != null)
        {
            if (nameDisplay.text == item.name)
            {
                item.Use();
            }
            
        }

        if (inventory.activeSelf && item != null)
        {
            itemName.text = item.name;
        }

        if (item == null)
        {
            itemName.text = "";
        }
    }

    //Adds the specific item to the slot
    public void AddItem(Item newItem)
    {
        item = newItem;

        GetComponent<Button>().interactable = true;

    }

    //Displays the item info
    public void DisplayItem()
    {
        ClearInfo();

        icon.SetActive(true);
        icon.GetComponent<Image>().sprite = item.icon;
        icon.GetComponent<Image>().enabled = true;


        nameDisplay.text = item.name;

        foreach (string sentence in item.description)
        {
            description.text += sentence;
        }
    }

    //Clears the item display info related things
    public void ClearInfo()
    {
        icon.GetComponent<Image>().sprite = null;
        icon.GetComponent<Image>().enabled = false;
        icon.SetActive(false);

        nameDisplay.text = "";

        description.text = "";
    }

    //Clears the actual item from the given inventory slot
    public void ClearSlot()
    {
        item = null;

        GetComponent<Button>().interactable = false;
    }
}

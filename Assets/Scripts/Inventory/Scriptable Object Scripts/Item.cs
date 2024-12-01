using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    new public string name = "New Item";
    public Sprite icon = null;
    [TextArea(1, 5)]
    public string[] description;
    public bool isDefaultItem = false;

    public virtual void Use()
    {
        //Use the item
        //Something might happen
    }

}

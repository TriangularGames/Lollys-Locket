using UnityEngine;

[CreateAssetMenu(fileName = "New Key", menuName = "Inventory/Key")]
public class Key: Item
{
    public GameObject door;
    public override void Use()
    {
        base.Use();
        //with the door object tell it the key was used
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "New Book", menuName = "Inventory/Book")]
public class Book : Item
{
    public ObjectDialogueTrigger bookText;
    public override void Use()
    {
        base.Use();
        bookText.TriggerDialogue();
    }
}

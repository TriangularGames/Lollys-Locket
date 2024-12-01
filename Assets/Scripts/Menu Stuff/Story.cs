using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Story", menuName = "Story")]
public class Story : ScriptableObject
{
    [TextArea(1,20)]
    public string story;
}

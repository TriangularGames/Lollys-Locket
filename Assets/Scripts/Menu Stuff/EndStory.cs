using System.Collections;
using TMPro;
using UnityEngine;

public class EndStory : MonoBehaviour
{
    public Story story;
    public TextMeshProUGUI display;
    private float delay = 0.07f;
    public GameObject menu;
    public GameObject exit;

    public void displayStory()
    {
        StartCoroutine("TypeStory");
    }

    IEnumerator TypeStory()
    {
        yield return new WaitForSeconds(1f);
        foreach (char letter in story.story)
        {
            if (char.IsPunctuation(letter))
            {
                display.text += letter;
                yield return new WaitForSeconds(delay * 3);
            }
            else
            {
                display.text += letter;
                yield return new WaitForSeconds(delay);
            }
        }
        menu.SetActive(true);
        exit.SetActive(true);

    }
}

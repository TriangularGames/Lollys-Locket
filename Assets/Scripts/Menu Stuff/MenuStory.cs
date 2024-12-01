using System.Collections;
using TMPro;
using UnityEngine;

public class MenuStory : MonoBehaviour
{
    public Story story;
    public TextMeshProUGUI display;
    private float delay = 0.07f;
    public MainMenu menu;

    public void skipText()
    {
        StopCoroutine("TypeStory");
        display.text = "";
        display.text = story.story;
        StartCoroutine("WaitToRead");
    }

    IEnumerator WaitToRead()
    {
        yield return new WaitForSeconds(7f);
        menu.StartCoroutine("StartGame");
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
        yield return new WaitForSeconds(5f);
        menu.StartCoroutine("StartGame");

    }
}

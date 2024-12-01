using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SavedGame : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI dateTime;

    [SerializeField]
    public GameObject timeText;

    [SerializeField]
    private Image character;

    [SerializeField]
    public Sprite characterImage;

    [SerializeField]
    public GameObject characterPortrait;

    [SerializeField]
    private int index;

    public int GetIndex { get => index;}

    public void ShowInfo(PlayerData data)
    {
        timeText.SetActive(true);
        characterPortrait.SetActive(true);

        dateTime.text = "Time: " + data.MyTime.ToString("H:mm");
        character.sprite = characterImage;
    }

}

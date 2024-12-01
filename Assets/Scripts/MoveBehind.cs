using UnityEngine;

public class MoveBehind : MonoBehaviour
{
    private GameObject player;
    public int sorting;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = sorting;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            player.GetComponent<SpriteRenderer>().sortingOrder = 5;
        }
    }
}

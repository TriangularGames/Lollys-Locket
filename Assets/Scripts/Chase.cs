using System.Collections;
using UnityEngine;

public class Chase : MonoBehaviour
{
    private GameObject player;
    private bool isChasing;

    private float speed = 0.9f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isChasing)
        {
            isChasing = true;
            player.GetComponent<PlayerMovement>().isGettingChased = true;
            GetComponent<AudioSource>().Play();
            StartCoroutine("ChasePlayer");
        }
    }

    IEnumerator ChasePlayer()
    {
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && isChasing)
        {
            endlife();
        }
    }

    void endlife()
    {
        Destroy(gameObject);
    }
}

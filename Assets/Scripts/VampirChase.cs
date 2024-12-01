using System.Collections;
using UnityEngine;

public class VampirChase : MonoBehaviour
{
    private float speed = 0.45f;
    private static int count = 0;

    private GameObject player;
    private bool isRunning;
    public bool started;

    public GameObject door;

    public void startChase()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine("ChasePlayer");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player" && !isRunning && started)
        {
            count++;
            speed += 0.2f * count;
            StartCoroutine("ChasePlayer");
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isRunning = false;
            StopCoroutine("ChasePlayer");
            door = player.GetComponent<PlayerInteraction>().pastDoor;
            useitdamnit();

        }
    }

    IEnumerator ChasePlayer()
    {
        isRunning = true;
        while (true)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    void useitdamnit()
    {
        StopCoroutine("UseDoor");
        StartCoroutine("UseDoor");
    }

    IEnumerator UseDoor()
    {
        yield return new WaitForSeconds(2f);
        transform.position = door.transform.position;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine("ChasePlayer");
    }
}

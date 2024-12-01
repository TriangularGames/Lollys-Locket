using System.Collections;
using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject toDoor;
    public GameObject Player;
    public Animator transition;
    public bool needsObject;
    public Item neededitem;
    private float transitionTime = 2f;


    public void goToDoor()
    {
        Player.transform.position = toDoor.transform.position;
    }

    public void go()
    {
        StartCoroutine("goTo");
    }

    IEnumerator goTo()
    {
        if (Inventory.instance.items.Contains(neededitem) && neededitem)
        {
            //Start Transition
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            Player.transform.position = toDoor.transform.position;

            //Finish transition
            transition.SetTrigger("End");
            yield return new WaitForSeconds(1f);
            Player.GetComponent<PlayerInteraction>().interacting = false;
        }
        else if (!neededitem)
        {
            //Start Transition
            transition.SetTrigger("Start");

            yield return new WaitForSeconds(transitionTime);

            Player.transform.position = toDoor.transform.position;

            //Finish transition
            transition.SetTrigger("End");
            Player.GetComponent<PlayerInteraction>().interacting = false;
            yield return new WaitForSeconds(1f);
        }


    }
}

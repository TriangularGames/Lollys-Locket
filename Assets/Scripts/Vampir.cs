using System.Collections;
using UnityEngine;

public class Vampir : MonoBehaviour
{
    private Animator an;

    private void Start()
    {
        an = GetComponent<Animator>();
        StartCoroutine("existence");
    }
    
    IEnumerator existence()
    {
        yield return new WaitForSeconds(3f);
        Awaken();
        while (!GameObject.FindGameObjectWithTag("DialogueManager").GetComponent<DialogueManager>().Finished)
        {
            yield return new WaitForEndOfFrame();
        }
        triggerTransformation();
    }

    public void Awaken()
    {
        GetComponent<DialogueTrigger>().TriggerDialogue();

    }

    void triggerTransformation()
    {
        an.SetTrigger("Transform");
    }

    public void triggerChase()
    {
        an.SetTrigger("Transform Over");
        Destroy(GetComponent<DialogueTrigger>());
        GetComponent<VampirChase>().started = true;
        GetComponent<VampirChase>().startChase();
    }
}

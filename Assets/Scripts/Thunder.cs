using System.Collections;
using UnityEngine;

public class Thunder : MonoBehaviour
{
    private float minWait = 10f;
    private float maxWait = 25f;
    private AudioSource AS;
    // Start is called before the first frame update
    void Start()
    {
        AS = this.GetComponent<AudioSource>();
        StartCoroutine("ThunderAndLightning");
    }

    IEnumerator ThunderAndLightning()
    {
        while (true)
        {
            AS.Play();
            yield return new WaitForSeconds(Random.Range(minWait, maxWait));
        }

    }
    
}

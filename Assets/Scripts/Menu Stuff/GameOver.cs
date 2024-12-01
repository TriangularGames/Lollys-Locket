using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Animator transition;
    private float transitionTime = 2f;
    public GameObject panel;

    public void MainMenu() {
        StartCoroutine("MainMenuTransition");
    }

    public void Exit() {
        Application.Quit();
    }

    IEnumerator MainMenuTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Hide Options Menu
        panel.SetActive(false);

        //Show Main Menu
        SceneManager.LoadScene(0);
    }

}

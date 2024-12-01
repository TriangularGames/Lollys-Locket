using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public Animator transition;
    private float transitionTime = 2f;

    //Main Menu Objects
    public GameObject MainMenuPanel;

    //Options Menu Objects
    public GameObject OptionsPanel;

    //Story screen
    public GameObject Story;


    public void playGame()
    {
        StartCoroutine("PlayGameTransition");
    }

    public void optionsMenu()
    {
        StartCoroutine("OptionsTransition");
    }

    public void mainMenu()
    {
        StartCoroutine("MainMenuTransition");
    }

    public void Exit()
    {
        Application.Quit();
    }

    //Option Transition
    IEnumerator OptionsTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Hide Main Menu
        MainMenuPanel.SetActive(false);

        //Show Options Menu
        OptionsPanel.SetActive(true);

        //Finish transition
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator MainMenuTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        //Hide Options Menu
        OptionsPanel.SetActive(false);

        //Show Main Menu
        MainMenuPanel.SetActive(true);

        //Finish transition
        transition.SetTrigger("End");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator PlayGameTransition()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);


        MainMenuPanel.SetActive(false);

        Story.SetActive(true);

        //Finish transition
        transition.SetTrigger("End");
        Story.GetComponent<MenuStory>().StartCoroutine("TypeStory");
        yield return new WaitForSeconds(1f);


    }

    IEnumerator StartGame()
    {
        //Start Transition
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //function to transition to Solitaire Game Scene
   public void PlaySolitaire()
    {
        SceneManager.LoadScene(1);
    }

    //Function to return to main menu
    public void PlayDevilsGrip()
    {
        SceneManager.LoadScene(2);
    }

    //Function to return to main menu
    public void MainMenuReturn()
    {
        SceneManager.LoadScene(0);
    }

    //Function to return to main menu
    public void QuiteApplication()
    {
        Application.Quit();
    }
}

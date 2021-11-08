using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PauseMenuScript : MonoBehaviour
{
    //the button that is used to pause the game
    public Button btn;
    //variable for checking if the game is paused or not
    public static bool gameIsPaused=false;
    //the UI for the pause menu
    public GameObject pauseMenuUI;
    //function for when the pause button is clicked
    public void buttonClicked(){
        //cheking if the game is paused
        if(gameIsPaused){
            //if the game is paused we call the resume function for returning to the game
            Resume();
        }
        else{
            //if the game is not paused we call the pause function to pause the game
            Pause();
        }
    }
    //function for resuming the game
    public void Resume(){
        //we deactivate the menu UI
        pauseMenuUI.SetActive(false);
        //we set the time scale to normal
        Time.timeScale=1f;
        gameIsPaused=false;
    }
    //function for pausing the game
    void Pause(){
        //we make the menu UI appear
        pauseMenuUI.SetActive(true);
        //we set the time scale to 0 so the time stops
        Time.timeScale=0f;
        gameIsPaused=true;
    }
}

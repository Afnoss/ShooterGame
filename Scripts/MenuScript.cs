using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    void Start(){
        Time.timeScale = 1;
    }
    
    public void PlayGame(){
        //we load the game scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        
    }
}

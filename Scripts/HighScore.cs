using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HighScore : MonoBehaviour
{
   public Text score;
   public Text highScore;
   void Start(){
      //we display the highscore 
      if(PlayerPrefs.GetInt("HighScore")>=PlayerPrefs.GetInt("Score")){
    highScore.text=PlayerPrefs.GetInt("HighScore").ToString();
   }
   else if(PlayerPrefs.GetInt("HighScore")<PlayerPrefs.GetInt("Score")){
      PlayerPrefs.SetInt("HighScore",PlayerPrefs.GetInt("Score"));
      highScore.text=PlayerPrefs.GetInt("HighScore").ToString();
   }
   
    
}
}

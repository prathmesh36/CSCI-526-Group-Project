using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LevelMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public void LevelButton(){
        DateTime currentTime = DateTime.Now;
        long unixTimestamp = (long)(currentTime - new DateTime(1970, 1, 1)).TotalSeconds;
        GameManager.startTime = unixTimestamp;
        GameManager.bombCount = 0;
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneManager.LoadScene("MyGame");
    }

     public void LoadTutorial(){
        GameManager.bombCount = 0;
        SceneManager.LoadScene("MainTutorial");
    }
}

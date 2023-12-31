using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class Spaceship : MonoBehaviour
{
    public float turnSpeed;
    // Start is called before the first frame update
    public int health=50;
    public TMP_Text healthDisplay;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward * turnSpeed * Input.GetAxisRaw("Horizontal") * Time.deltaTime);
        
    }
    public void TakeDamage()
    {
        health = health - 10;
        

        if (health <= 0)
        {
            
            // To do : Lost the mini game so something has to be shown here
            PlayerPrefs.SetInt("SpaceGerms", -1);
            

            SceneManager.LoadScene("MyGame");
        }
        healthDisplay.text = "Health: " + health + "/100";
        healthDisplay.color = new Color(1f, 0f, 0f);
    }
    public void AddHealth()
    {
        health = health + 10;
        if (health >= 100)
        {
            int monstersKilled = PlayerPrefs.GetInt(Constants.MONSTERS_KILLED);
            int monstersCount = PlayerPrefs.GetInt(Constants.TOTAL_MONSTERS);
            Debug.Log(string.Format("Monsters Consumed so far: {0}", monstersKilled));
            Debug.Log(string.Format("Monsters Encountered so far: {0}", monstersCount));  
            recordAnalyticsForSpaceMonster(monstersKilled,monstersCount);  

            SceneManager.LoadScene("YouWonMiniSpaceGermsGame");
            PlayerPrefs.SetInt("SpaceGermsPuzzle", 1);

            // Reset the value for space monsters
            PlayerPrefs.SetInt(Constants.MONSTERS_KILLED, 0);

            //To Do: Won the mini game. So health has to be increased

            //Unnati: Who set the wrong key for player prefs? Wrong key was set for Playerprefs and this error took me 2 hours to understand
            PlayerPrefs.SetInt("SpaceGerms", 1);
            Debug.Log("Unnati: calling from the spaceship.cs (First game) and the playerprefs have been set to:" + PlayerPrefs.GetInt("SpaceGerms"));
            SceneManager.LoadScene("MyGame");
        }
        healthDisplay.text = "Health: " + health + "/100";
        healthDisplay.color = new Color(0f, 1f, 0.282f);

    }

    public void recordAnalyticsForSpaceMonster(int monstersKilled, int monstersCount){
            SpaceMonsterAnalytics spaceMonsterAnalytics = new SpaceMonsterAnalytics();
            spaceMonsterAnalytics.monstersKilled = monstersKilled;
            spaceMonsterAnalytics.monstersCount = monstersCount;
            string json = JsonUtility.ToJson(spaceMonsterAnalytics);
            Analytics.Instance.SaveData("space-monster-game-data.json", json);
            Debug.Log("Analytics for Space Monster recorded!");
    }
 
}

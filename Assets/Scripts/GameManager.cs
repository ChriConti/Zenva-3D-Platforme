using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    //static instance of the game manager,
    //can be access from anywhere
    public static GameManager instance = null;

    //player  score
    public int score = 0;

    public int highScore = 0;

    public int currentLevel = 1;

    public int highestLevel = 2;

    void Awake()
    {
        //if it doesn't exist
        if (instance == null)
        {
            //set the instance to the current object (this)
            instance = this;
        }
        //there can onlu be a single instance of the game manager
        else if (instance != this)
        {
            //destroy the current object, so there is just one manager
            Destroy(gameObject);
        }
        //don't destory this object when loading scenes
        DontDestroyOnLoad(gameObject);
    }

    //increase score
    public void IncreaseScore()
    {
        // increase the score by the given amount
        score ++;

        //show the new score in the console
        print("New Score: " + score.ToString());

        if(score > highScore)
        {
            highScore = score;
            print("New high score: " + highScore);
        }
    }
    
    public void Reset()
    {
        //reset score
        score = 0;

        currentLevel = 1;

        SceneManager.LoadScene("Level" + currentLevel);
    }

    public void IncreaseLevel()
    {
        if (currentLevel < highestLevel)
        {
            currentLevel++;
        }
        else
        {
            currentLevel = 1;
        }
        SceneManager.LoadScene("Level" + currentLevel);
    }
}
 
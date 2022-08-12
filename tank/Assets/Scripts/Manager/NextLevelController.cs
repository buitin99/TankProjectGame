using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelController : MonoBehaviour
{
    public GameObject nextLevelMenu;
    private int currentLevel = 1;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        nextLevelMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       if(PlayerController.totalScore == 1)
        {
             NextGameOrPlayAgain();
        }
    }

    public void NextGameOrPlayAgain()
    {
        nextLevelMenu.SetActive(true);
    }

    public void GoToPlayAgain()
    {      
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoToNextLevel()
    {
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex + currentLevel));
        PlusCurrentLevel();
    }

    public void PlusCurrentLevel()
    {
        currentLevel++;  
    }    

   
}

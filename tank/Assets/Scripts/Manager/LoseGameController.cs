using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseGameController : MonoBehaviour
{
    public GameObject loseMenu;
    public static bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        loseMenu.SetActive(false);
    }

    void Update()
    {
       if(PlayerController.isLose)
        {
            PlayAgainOrBackToMenu();
        }
    }

    public void PlayAgainOrBackToMenu()
    {
        loseMenu.SetActive(true);
    }
    public void GoToMainMenu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }    

    public void GoToPlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }    



}

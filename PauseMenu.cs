using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GameObject deathScreenUI;
    public PlayerMovement playerInfo;

    // Pause Menu Animator
    private Animator pauseAnim;

    private void Start()
    {
        pauseMenuUI.SetActive(false);
        deathScreenUI.SetActive(false);
        Time.timeScale = 1f;

        // Store the animator for the pause menu
        pauseAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
        if (playerInfo.currentHealth <= 0)
        {
            deathScreenUI.SetActive(true);
            pauseAnim.SetBool("isOpen", true);
            Time.timeScale = 0f;
        }
    }

    public void Pause()
    {
        /*
        Time.timeScale = 0f;
        GameIsPaused = true;*/
        pauseMenuUI.SetActive(true);
        pauseAnim.SetBool("isOpen", true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    void Resume()
    {
        /*
        Time.timeScale = 1f;
        GameIsPaused = false;*/
        pauseMenuUI.SetActive(false);
        pauseAnim.SetBool("isOpen", false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void QuitToTitle()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;
    }

}
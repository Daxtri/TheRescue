using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public  bool paused;
    public GameObject menuCanvas;
    // Start is called before the first frame update
    void Start()
    {
        paused = false;
        menuCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            paused = !paused;

            if (paused == true)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        menuCanvas.SetActive(false);
        Time.timeScale = 1f;
        paused = false;
    }

    public void Pause()
    {
        menuCanvas.SetActive(true);
        Time.timeScale = 0f;
        paused = true;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}

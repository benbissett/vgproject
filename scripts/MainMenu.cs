using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public Button play;
    public Button quit;
    public Button cont;

    void Start()
    {
        play.onClick.AddListener(PlayOnClick);
        quit.onClick.AddListener(TaskOnClick);
        cont.onClick.AddListener(ContOnClick);
        if (PlayerPrefs.HasKey("Level")) {
            cont.gameObject.SetActive(true);
        }
        else
        {
            cont.gameObject.SetActive(false);
        }
    }

    private void PlayOnClick()
    {
        SceneManager.LoadScene("Main");
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("EnemyLevel", 1);
    }

    private void TaskOnClick()
    {
        Application.Quit();
    }

    private void ContOnClick()
    {
        if (PlayerPrefs.GetInt("Stage") == 2)
        {
            SceneManager.LoadScene("Level2");
        }
        else
        {
            SceneManager.LoadScene("Main");
        }
    }
}

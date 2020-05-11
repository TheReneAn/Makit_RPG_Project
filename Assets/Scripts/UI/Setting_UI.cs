using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Setting_UI : MonoBehaviour
{
    [Header("Setting Information")]
    private bool isPaused;
    public GameObject pausePanel;

    [Header("Audio")]
    private AudioManager theAudio;
    public string Click_sound;

    void Start()
    {
        theAudio = FindObjectOfType<AudioManager>();

        isPaused = false;
    }

    public void ChangePause()
    {
        theAudio.Play(Click_sound);

        isPaused = !isPaused;
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void QuitToMain()
    {
        theAudio.Play(Click_sound);
        GameManager.Instance.SceneChange("MainScreen");
        Time.timeScale = 1f;
    }
}

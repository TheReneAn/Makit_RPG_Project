using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    public void MainMenu()
    {
        GameManager.Instance.SceneChange("MainScreen");
    }
    public void Quit()
    {
        Application.Quit();
    }
}

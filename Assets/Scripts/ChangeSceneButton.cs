using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSceneButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SceneField()
    {
        GameManager.Instance.g_CheckSceneChange = true;
        GameManager.Instance.SceneChange("Field");
    }
    public void SceneVillage()
    {
        GameManager.Instance.SceneChange("Village");
    }
}

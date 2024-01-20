using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISceneLoader : MonoBehaviour
{
    public void LoadScene(string scene)
    {
        SceneLoader.Load(scene);
    }


    public void ExitButton()
    {
        Debug.Log("Game closed");
        Application.Quit();
    }
}

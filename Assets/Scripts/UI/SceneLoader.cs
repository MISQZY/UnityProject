using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{

    private class LoadingMonoBehaviour: MonoBehaviour { }


    private static Action onLoaderCallback;
    private static AsyncOperation loadingAsyncOperation;

    public static void Load(string scene)
    {
        onLoaderCallback = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Game Object");
            if(SceneManager.GetSceneByName(scene) != null)
            { 
            loadingGameObject.AddComponent<LoadingMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
            }
        };

        SceneManager.LoadScene("LoadingScene");
    }

    private static IEnumerator LoadSceneAsync(string scene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene);

        while (!asyncOperation.isDone) 
        {
            yield return null;
        }
    }

    public static float GetLoadProgress()
    {
        if(loadingAsyncOperation != null)
        {
            return loadingAsyncOperation.progress;
        } 
        else
        { 
            return 1f; 
        }
    }


    public static void LoaderCallback()
    {
        if(onLoaderCallback != null)
        {
            onLoaderCallback();
            onLoaderCallback = null;
        }
    }
}

using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    /**
     * Loads scene
     */
    public static bool Load(string scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        Debug.Log(scene);
        var loadLevelOperation = SceneManager.LoadSceneAsync(scene, mode);
        loadLevelOperation.allowSceneActivation = true;
        return true;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    public void LoadSceneFromLevelManager(string level)
    {
        SceneManager.LoadScene(level,LoadSceneMode.Single);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

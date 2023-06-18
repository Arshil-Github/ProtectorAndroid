using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurvivalSceneManager : MonoBehaviour
{

    public void Restart()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(SceneManager.GetActiveScene().buildIndex);
    }
    public void Home()
    {
        GameObject.Find("LevelLoader").GetComponent<levelloader>().LoadNextLevel(0);
    }
}

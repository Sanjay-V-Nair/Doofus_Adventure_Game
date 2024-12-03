using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
    private string singlePlayerScreen = "Game_Scene";

    public void SinglePlayer() {
        SceneManager.LoadScene(singlePlayerScreen);
    }
    public void Quit() {
        Application.Quit();
    }
}

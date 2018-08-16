using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public void  StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Levels()
    {
        SceneManager.LoadScene(4);
    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit();
    }
}

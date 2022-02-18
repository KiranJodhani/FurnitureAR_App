using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("LoadMainMenu", 0.1f);
    }

    void LoadMainMenu()
	{
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
	}
}

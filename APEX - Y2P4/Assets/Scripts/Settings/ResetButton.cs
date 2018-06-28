using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

	public void ResetGame()
	{
		SceneManager.LoadScene(0);
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}

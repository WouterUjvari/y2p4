using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResetButton : MonoBehaviour {

	public void ResetGame()
	{
        VRPlayerMovementManager.instance.ToggleCanMove(true);
		SceneManager.LoadScene(1);
	}

    public void QuitGame()
    {
        Application.Quit();
    }
}

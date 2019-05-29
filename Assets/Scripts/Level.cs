using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{

	[SerializeField] float delayInSeconds = 2f;
	public void LoadStartMenu(){
		SceneManager.LoadScene(0);
	}

	public void LoadGame(){
		Debug.Log("game loaded");
		SceneManager.LoadScene("Game");
	}

	public void LoadGameOver(){
		StartCoroutine(LoadGameOverCoroutine());
	}

	IEnumerator LoadGameOverCoroutine(){
		yield return new WaitForSeconds(delayInSeconds);
		SceneManager.LoadScene("Game Over");
	}

	public void QuitGame(){
		Application.Quit();
	}

}

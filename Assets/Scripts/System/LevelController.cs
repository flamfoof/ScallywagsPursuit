using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour {


	public bool isPaused = false;

	public GameObject Player;

	public GameObject pauseMenu;
	public GameObject islandMenu;
	public GameObject salvageMenu;


	void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Enter"))
		{
			Pause ();
		}
	}
	public void Pause()
	{
		if (pauseMenu != null) {
			isPaused = !isPaused;
			if (isPaused) {
				Time.timeScale = 0;
				pauseMenu.SetActive (true);
			} else {
				Time.timeScale = 1;
				pauseMenu.SetActive (false);
			}

		}
	}


	public void Resume()
	{
		isPaused = !isPaused;
		Time.timeScale = 1;
		pauseMenu.SetActive (false);
	}


	public void Quit()
	{
		Application.Quit();
	}


	public void ChangeScene(int scene)
	{
		SceneManager.LoadScene(scene);
		if (isPaused) {
			Resume ();
		}
	}

	public void toggleObject(GameObject obj)
	{
		obj.SetActive(!obj.activeInHierarchy);
	}



	//Everything below is all islands/everything else.

	//this opens the island menu
	public void Loot()
	{
		Player.GetComponent<PlayerInteraction> ().Loot ();
	}


	public void CloseIslandMenu()
	{
		isPaused = !isPaused;
		Time.timeScale = 1;
		islandMenu.SetActive (false);
	}

	public void CloseSalvageMenu()
	{
		isPaused = !isPaused;
		Time.timeScale = 1;
		salvageMenu.SetActive (false);
	}

	public void Victory(int sceneLevel)
	{
		SceneManager.LoadScene(sceneLevel);
	}

	//salvage functions
	public void Salvage()
	{
		Player.GetComponent<PlayerInteraction> ().Salvage();
	}
}

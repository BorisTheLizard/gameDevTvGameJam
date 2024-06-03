using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenuButtons : MonoBehaviour
{
	[SerializeField] GameObject settings;
	timeController _time;
	[SerializeField] GameObject gamePlayMenu;
	[SerializeField] bool isMainMenu;
	public bool gameplayMenuIsopen;

	private void Awake()
	{
		_time = FindObjectOfType<timeController>();
	}

	public void loadMainMenu()
	{
		_time.setTime(1);
		SceneManager.LoadScene(0);
	}


	public void exitGame()
	{
		Application.Quit();
	}

	public void callSettings()
	{
		settings.SetActive(true);
		gamePlayMenu.SetActive(false);
	}

	public void returnFromGamePlayMenu()
	{
		_time.setTime(1);
		gamePlayMenu.SetActive(false);
		gameplayMenuIsopen = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (!isMainMenu)
			{
				if (!gameplayMenuIsopen)
				{
					openGamePlayMenu();
					gameplayMenuIsopen = true;
				}
				else
				{
					returnFromGamePlayMenu();
					settings.SetActive(false);
					//gameplayMenuIsopen = false;
				}
			}

		}
	}
	private void openGamePlayMenu()
	{
		_time.setTime(0);
		gamePlayMenu.SetActive(true);
	}
}

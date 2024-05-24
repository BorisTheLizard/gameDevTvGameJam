using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuController : MonoBehaviour
{
	[SerializeField] GameObject mainMenuHolder;
	[SerializeField] GameObject settingsScreenHolder;
	[SerializeField] GameObject gameplayUi;
	private bool inMenu = false;
	public bool isMainSreen;

	public void openSettings()
	{
		settingsScreenHolder.SetActive(true);

		if (!isMainSreen)
		{
			gameplayUi.SetActive(false);
			timeController();
		}
		else
		{
			mainMenuHolder.SetActive(false);
		}
	}

	public void closeSettings()
	{
		settingsScreenHolder.SetActive(!true);

		if (!isMainSreen)
		{
			gameplayUi.SetActive(true);
			timeController();
		}
		else
		{
			mainMenuHolder.SetActive(!false);
		}
	}

	private void timeController()
	{
		if (!inMenu)
		{
			inMenu = true;
			Time.timeScale = 0;
		}
		else
		{
			inMenu = !true;
			Time.timeScale = 1;
		}
	}
}

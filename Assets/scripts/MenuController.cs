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
	[SerializeField] timeController _timeController;
	[SerializeField] pauseMenuButtons pb;
	//private bool inMenu = false;
	public bool isMainSreen;

	public void openSettings()
	{
		settingsScreenHolder.SetActive(true);

		if (!isMainSreen)
		{
			gameplayUi.SetActive(false);
			//timeController();
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
			_timeController.setTime(1);
			pb.gameplayMenuIsopen = false;
			//timeController();
		}
		else
		{
			mainMenuHolder.SetActive(!false);
			
		}
	}

/*	private void timeController()
	{
		if (!inMenu)
		{
			inMenu = true;

			_timeController.setTime(0);

			if (_timeController.isSlowTime)
			{
				_timeController.isSlowTime = false;
			}
		}
		else
		{
			inMenu = !true;

			_timeController.setTime(1);
		}
	}*/
}

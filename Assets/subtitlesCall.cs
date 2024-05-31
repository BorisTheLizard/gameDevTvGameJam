using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class subtitlesCall : MonoBehaviour
{
	[SerializeField] GameObject subtitles;
	[SerializeField] GameObject mainMenu;
	[SerializeField] GameObject returnButton;

	public void callSubtitlesOn()
	{
		subtitles.SetActive(true);
		mainMenu.SetActive(false);
		returnButton.SetActive(true);
	}
	public void callSubtitlesOff()
	{
		subtitles.SetActive(!true);
		mainMenu.SetActive(!false);
		returnButton.SetActive(!true);
	}
}

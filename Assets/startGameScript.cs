using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGameScript : MonoBehaviour
{
	[SerializeField] GameObject mainMenu;
	[SerializeField] Animator anim;
	[SerializeField] GameObject timeLine;
	[SerializeField] GameObject fadeIn;

	[SerializeField] sceneManagement _sm;
	public void startGame()
	{
		anim.SetTrigger("off");
		timeLine.SetActive(true);
	}

	public void launchLvl1()
	{
		_sm.LoadNextLevel();
	}
	public void fade()
	{
		fadeIn.SetActive(true);
	}
}

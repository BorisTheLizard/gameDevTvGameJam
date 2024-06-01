using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restoreBossAndPlayer : MonoBehaviour
{
	[SerializeField] GameObject boss;
	[SerializeField] GameObject player;
	[SerializeField] GameObject cutscen;
	[SerializeField] GameObject cutScreen;


	public void restoreBossAndPlayerFunk()
	{
		boss.SetActive(true);
		player.SetActive(true);
		player.GetComponent<playerController>().moveSpeed = 10;
		cutscen.SetActive(false);
		cutScreen.SetActive(false);
		player.GetComponent<AttackSystem>().isCooldown = false;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			restoreBossAndPlayerFunk();
		}
	}
}

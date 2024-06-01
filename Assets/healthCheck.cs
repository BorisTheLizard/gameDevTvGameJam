using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthCheck : MonoBehaviour
{
	[SerializeField] healthSystem health;
	bool isEnded = false;
	[SerializeField] GameObject player;
	[SerializeField] AudioSource playerSource;
	[SerializeField] AudioClip lastClip;
	[SerializeField] GameObject lastCutscene;
	[SerializeField] GameObject credits;
	[SerializeField] GameObject fadeIn;
	[SerializeField] GameObject fadeOut;
	[SerializeField] GameObject cutScreen;
	[SerializeField] GameObject skipText;

	private void FixedUpdate()
	{
		if (health.Health <= 0 && !isEnded)
		{
			StartCoroutine(ENDIT());
		}
	}
	IEnumerator ENDIT()
	{
		isEnded = true;
		yield return new WaitForSeconds(3);
		player.GetComponent<AttackSystem>().enabled = false;
		playerSource.pitch = 1;
		playerSource.PlayOneShot(lastClip);
		yield return new WaitForSeconds(2);
		fadeIn.SetActive(true);
		yield return new WaitForSeconds(3);
		player.SetActive(false);
		fadeIn.SetActive(!true);
		cutScreen.SetActive(true);
		skipText.SetActive(false);
		fadeOut.SetActive(true);
		lastCutscene.SetActive(true);
		yield return new WaitForSeconds(5);
		credits.SetActive(true);
	}
	//YA EBAL ETI VASHI GAME JAMS!!!!!!!!!!!!!!
}

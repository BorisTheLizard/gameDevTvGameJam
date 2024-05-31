using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endLvl1 : MonoBehaviour
{
	[SerializeField] GameObject endCutscene;
	[SerializeField] GameObject player;
	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			player.SetActive(false);
			endCutscene.SetActive(true);
			this.enabled = false;
		}
	}
}

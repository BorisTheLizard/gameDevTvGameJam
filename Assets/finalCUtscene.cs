using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class finalCUtscene : MonoBehaviour
{
	[SerializeField] GameObject bossFightCutscene;
	[SerializeField] GameObject boss;
	[SerializeField] GameObject player;
	[SerializeField] BoxCollider col;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			bossFightCutscene.SetActive(true);
			boss.SetActive(false);
			player.SetActive(false);
			col.enabled = false;
		}
	}
}

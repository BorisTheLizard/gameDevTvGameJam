using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lvl2EndLvlTrig : MonoBehaviour
{
	[SerializeField] GameObject endLvlCutScene;
	[SerializeField] GameObject playerHorseRideModel;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			endLvlCutScene.SetActive(true);
			playerHorseRideModel.SetActive(false);
		}
	}
}

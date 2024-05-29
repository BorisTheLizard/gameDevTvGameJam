using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canControl : MonoBehaviour
{
	[SerializeField] horseController _controller;
	[SerializeField] raycastShooting _shooting;


	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Player")
		{
			_controller.enabled = true;
			_shooting.enabled = true;
			this.gameObject.SetActive(false);
		}
	}
}

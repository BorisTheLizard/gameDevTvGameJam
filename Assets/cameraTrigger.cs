using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class cameraTrigger : MonoBehaviour
{
	[SerializeField] CinemachineVirtualCamera cam;
	private void OnTriggerStay(Collider other)
	{
		if (other.tag == "Player")
		{
			cam.Priority = 100;
		}
	}
	private void OnTriggerExit(Collider other)
	{
		if (other.tag == "Player")
		{
			cam.Priority = 0;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reloadLvl : MonoBehaviour
{
	sceneManagement _SM;
	private void Awake()
	{
		_SM = FindObjectOfType<sceneManagement>();
	}
	private void OnEnable()
	{
		_SM.restartScene();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hideOnStart : MonoBehaviour
{
	[SerializeField] MeshRenderer mat;

	private void Start()
	{
		mat.enabled = false;
	}
}

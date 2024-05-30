using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCursor : MonoBehaviour
{
	[SerializeField] GameObject targetPoint;

	private void FixedUpdate()
	{
		transform.LookAt(targetPoint.transform.position);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(fieldOfView))]
public class fieldOfViewEditor : Editor
{
	private void OnSceneGUI()
	{
		fieldOfView fow = (fieldOfView)target;
		Handles.color = Color.white;
		Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
		Vector3 viewAngleA = fow.dirFromAngle(-fow.viewAngle / 2, false);
		Vector3 viewAngleB = fow.dirFromAngle(fow.viewAngle / 2, false);

		Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
		Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

/*		Handles.color = Color.red;
		foreach (Transform visibleTargets in fow.visibleTargets)
		{
			Handles.DrawLine(fow.transform.position, visibleTargets.position);
		}*/
	}
}

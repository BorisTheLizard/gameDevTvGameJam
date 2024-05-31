using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class fieldOfView : MonoBehaviour
{
	public float viewRadius;
	[Range(0,360)] public float viewAngle;
	public List<Transform> visibleTargets = new List<Transform>();
	public LayerMask playerLayer;
	public LayerMask obstacleLayer;
	public bool seePlayer = false;


	private void OnEnable()
	{
		StartCoroutine(findTargetsWithDelay(.3f));
	}

	IEnumerator findTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			findVisibleTargets();
		}
	}

	void findVisibleTargets()
	{
		visibleTargets.Clear();

		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, playerLayer);

		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;

			float distance = Vector3.Distance(transform.position, target.position);

			Vector3 dirToTarget = (target.position - transform.position).normalized;

			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{
				float distToTarget = Vector3.Distance(transform.position, target.position);

				RaycastHit hitInfo;

				// Perform the raycast
				if (!Physics.Raycast(transform.position, dirToTarget, out hitInfo, distToTarget, obstacleLayer))
				{
					visibleTargets.Add(target);

					if (visibleTargets.Contains(target))
					{
						seePlayer = true;
					}
				}
			}
		}

		if (visibleTargets.Count < 1)
		{
			seePlayer = false;
		}

	}

	public Vector3 dirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal)
		{
			angleInDegrees += transform.eulerAngles.y;
		}
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}

	public void dumpData()
	{
		visibleTargets.Clear();
		seePlayer = false;
	}
}

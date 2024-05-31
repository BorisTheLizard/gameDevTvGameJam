using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class explosionOnEnable : MonoBehaviour
{
	CinemachineImpulseSource source;
	[SerializeField] float radius = 20f;
	[SerializeField] LayerMask layersToaffect;
	[SerializeField] LayerMask obstacleLayer;



	[SerializeField] float noiseRadius = 20;
	[SerializeField] LayerMask enemyLayer;

	private void Awake()
	{
		source = GetComponent<CinemachineImpulseSource>();
	}
	private void OnEnable()
	{
		source.GenerateImpulse();

		makeNoise();

		Vector3 explosionPos = transform.position;

		Collider[] colliders = Physics.OverlapSphere(explosionPos, radius, layersToaffect);

		for (int i = 0; i < colliders.Length; i++)
		{
			Transform target = colliders[i].transform;

			Vector3 dirToTarget = (target.position - transform.position).normalized;

			float distToTarget = Vector3.Distance(transform.position, target.position);

			if (!Physics.Raycast(transform.position, dirToTarget, distToTarget, obstacleLayer))
			{
				if (target.tag=="Player")
				{
					target.GetComponent<healthSystem>().TakeDamage(1);
					target.GetComponent<healthSystem>().TakeDamage(1);
				}
				if (target.tag == "enemy")
				{
					target.GetComponent<healthSystem>().TakeDamage(2);
				}
				if (target.tag == "destr")
				{
					target.GetComponent<destroyObjectScript>().destroyIt();
					target.GetComponent<destroyObjectScript>().gameObject.GetComponent<BoxCollider>().enabled = false;
				}
			}
		}
	}

	private void makeNoise()
	{
		Collider[] col = Physics.OverlapSphere(transform.position, noiseRadius, enemyLayer);

		foreach (var item in col)
		{
			if (item.GetComponent<topDownAI>() != null)
			{
				item.GetComponent<topDownAI>().heardNoise();
			}
		}
	}
}
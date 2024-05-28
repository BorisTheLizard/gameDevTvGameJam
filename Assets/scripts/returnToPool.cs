using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class returnToPool : MonoBehaviour
{
	GameObjectPool pool;
	public float TimeToWait;

	private void Awake()
	{
		pool = FindObjectOfType<GameObjectPool>();
	}
	private void OnEnable()
	{

			StartCoroutine(returnObj(TimeToWait));
		
	}

	IEnumerator returnObj(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		pool.ReturnObject(this.gameObject);
	}
}


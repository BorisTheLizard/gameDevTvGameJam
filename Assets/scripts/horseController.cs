using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horseController : MonoBehaviour
{
	[SerializeField] float xDistance;
	[SerializeField] GameObject horse;
	[SerializeField] GameObject horseHolder;
	[SerializeField] float moveSpeed;

	[SerializeField] GameObject rayCastTransform;
	[SerializeField] float rayLength;
	[SerializeField] LayerMask groundLayer;

	

	private void Update()
	{
		moveHorse();
		//gravity();
	}

	private void moveHorse()
	{
		float xMove = Input.GetAxis("Horizontal");
		float offset = xMove * Time.deltaTime * moveSpeed;
		float newPos = horse.transform.localPosition.x + offset;

		float clampedPos = Mathf.Clamp(newPos, -xDistance, xDistance);

		horse.transform.localPosition = new Vector3(clampedPos, 0, 0);
	}
/*	private void gravity()
	{
		RaycastHit hitInfo;

		if (!Physics.Raycast(rayCastTransform.transform.position, -rayCastTransform.transform.up,out hitInfo, rayLength, groundLayer))
		{
			horseHolder.transform.position = new Vector3(horseHolder.transform.position.x, horseHolder.transform.position.y + 1 * Time.deltaTime, horseHolder.transform.position.z);
		}
		else
		{
			horseHolder.transform.position = new Vector3(horseHolder.transform.position.x,hitInfo.point.y, horseHolder.transform.position.z);
		}
		//Debug.Log(hitInfo.transform.gameObject.name);
	}*/
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyChaseRayCast : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float rayLength = 10f;
    [SerializeField] Transform castTransform;
    [SerializeField] LayerMask obstaclesLayer;
    public bool isJumping = false;
    [SerializeField] GameObject enemyModel;
    [SerializeField] GameObject jumpModel;

    void Update()
    {
		if (!isJumping)
		{
            PlayJumpAnim();
        }
    }

    void PlayJumpAnim()
    {
        RaycastHit hit;
        Vector3 rayDirection = castTransform.forward;
        Debug.DrawRay(castTransform.position, rayDirection * rayLength, Color.red);

        if (Physics.Raycast(castTransform.position, rayDirection, out hit, rayLength, obstaclesLayer))
        {
            //Debug.Break();
            anim.SetTrigger("jump");
            isJumping = true;
            StartCoroutine(notJumping());

			if (hit.collider.gameObject.tag == "end")
			{
                jumpOnTrainAnim();
			}
        }
    }
    IEnumerator notJumping()
	{
        yield return new WaitForSeconds(0.5f);
        isJumping = false;
	}

    public void jumpOnTrainAnim()
	{
        enemyModel.SetActive(false);
        jumpModel.SetActive(true);
	}
}
